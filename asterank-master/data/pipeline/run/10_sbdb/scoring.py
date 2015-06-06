#
# Scoring function for asteroid objects
#
#from bigfloat import *   # TODO use this
import math
import random
import estimate

DEFAULT_RADIUS = .5  # km
DEFAULT_MASS = 1.47e15  # kg
DEFAULT_MOID = 2  # TODO get avg moid
DEFAULT_DV = 12#6.5 #km/s
DEFAULT_COMET_DV = 50  # km/s
DEFAULT_ALBEDO = .15
DEFAULT_DENSITY = 2 # g / cm^3

# g/cm^3
# https://en.wikipedia.org/wiki/Standard_asteroid_physical_characteristics#Density
TYPE_DENSITY_MAP = {
  'C': 1.38,
  'D': 1.38,
  'P': 1.38,
  'T': 1.38,
  'B': 1.38,
  'G': 1.38,
  'F': 1.38,
  'S': 2.71,
  'K': 2.71,
  'Q': 2.71,
  'V': 2.71,
  'R': 2.71,
  'A': 2.71,
  'M': 5.32,
}

def closeness_weight(obj):
  if obj['spec'] == 'comet':
    return -1

  emoid = DEFAULT_MOID if isinstance(obj['moid'], basestring) else obj['moid']

  # penalize aphelion distance
  aph = obj['ad']
  if aph > 50:
    return -1
  aph_score = 1/(1+math.exp(0.9*aph))

  major_axis = obj['a']
  ma_score = 1/(1+math.exp(0.45*major_axis))

  ph = obj['q']
  ph_score = 1/(1+math.exp(0.9*ph))

  if 'dv' in obj:
    dv = obj['dv']
  else:
    if obj['spec'] == 'comet':
      dv = DEFAULT_COMET_DV
    else:
      dv = DEFAULT_DV
      #return 0      # closeness shouldn't influence rank
  dv_score = 1 + (1/(1+math.exp(1.3*dv-6)))

  return pow(aph_score + ma_score + ph_score + 50*dv_score + 1, 2)

def price(obj):
  """
  Returns a tuple of $ price estimates for:
    0. Asteroid value per kg in raw materials.
    1. Asteroid $ saved per kg versus sending it up from Earth.
  """
  G = 6.67300e-20   # km^3 / kgs^2
  if obj['spec'] == 'comet':
    return (-1, -1)

  # estimate albedo
  if isinstance(obj['albedo'], basestring):
    albedo = DEFAULT_ALBEDO
  else:
    albedo = float(obj['albedo'])

  # estimate diameter
  if isinstance(obj['diameter'], basestring):
    if isinstance(obj['H'], basestring):
      # Can't estimate diameter :(
      diameter = DEFAULT_RADIUS * 2
    else:
      # Compute diameter in meters
      abs_magnitude = float(obj['H'])
      #diameter = 1329 * 10 ** (-abs_magnitude/5) * albedo ** (-1/2)
      diameter = 1329 / math.sqrt(albedo) * (10 ** (-0.2 * abs_magnitude))
      obj['est_diameter'] = diameter # keep as km

  # mass in kg
  exactmass = False
  if isinstance(obj['GM'], basestring):
    diameter = obj['est_diameter'] if 'est_diameter' in obj else obj['diameter']
    if diameter:
      # Use diameter to estimate mass --> estimate price
      # Pick density based on spectral type
      general_spec_type = obj['spec'][0].upper()
      if general_spec_type in TYPE_DENSITY_MAP:
        assumed_density = TYPE_DENSITY_MAP[general_spec_type]
      else:
        assumed_density = DEFAULT_DENSITY

      # Compute mass form density and diameter
      # FIXME assuming a perfect sphere for now...
      assumed_vol = 4/3 * math.pi * ((diameter / 2) ** 3)
      # Volume: km^3
      # Density: g/cm^3
      mass = assumed_vol * assumed_density / 6 * 1e15
    else:
      mass = DEFAULT_MASS
      obj['inexact'] = True
      mass = mass + (random.random() - .5) * 1e14   # some random factor
  else:
    exactmass = True
    mass = obj['GM'] / G

    if mass > 1e18:
      # if it's huge, penalize it because the surface will be covered in ejecta, etc.
      # and the goodies will be far beneath. Also, gravity well.
      mass = mass * 1e-6

  stype = obj['spec']
  value = estimate.valuePerKg(stype) * mass
  saved = estimate.savedPerKg(stype) * mass
  return (value, saved)

def profit(obj):
  if obj['spec'] == 'comet':
    return -1
  my_dv = obj['dv'] if 'dv' in obj else DEFAULT_DV
  return obj['price'] / 12 * obj['closeness'] / 3417.5490736698116 * estimate.profitRatio(DEFAULT_DV, my_dv)
