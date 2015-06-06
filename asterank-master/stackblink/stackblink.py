import sys
import os
import random
import pymongo
from pymongo import Connection
from redis import StrictRedis
sys.path.insert(0, os.path.join(os.path.dirname(__file__), ".."))

REDIS_STACKBLINK_PREFIX = 'stackblink:'
REDIS_COUNT_KEY = REDIS_STACKBLINK_PREFIX + 'total_image_count'

connection = Connection('localhost', 27017)
db = connection.asterank
asteroids = db.asteroids
stackblink = db.stackblink
stackblink_results = db.stackblink_results

redis = StrictRedis(host='localhost', port=6379, db=3)

def get_control_groups():
  # Returns a group of images for stacking/blinking

  # Choose a random target
  count = stackblink.count()
  control_object = stackblink.find({}, {'_id': False}).limit(-1) \
      .skip(random.randint(0, count-1)).next()
  # TODO  handle no groups
  return control_object

def get_unknown_group():
  # TODO
  pass

def record(email, image_keys, interesting, poor_quality):
  total_count = redis.incr(REDIS_COUNT_KEY, min(10, len(image_keys)))
  group_key = '||'.join(image_keys)
  stackblink_results.insert({
    'email': email,
    'group_key': group_key,
    'interesting': interesting,
    'poor_quality': poor_quality,
    })

  return {'success': True, 'count': total_count}

def get_image_count():
  val = redis.get(REDIS_COUNT_KEY)
  if not val:
    redis.set(REDIS_COUNT_KEY, 0)
    return 0
  return int(val)

def get_interesting_count():
  return stackblink_results.find({'interesting': True}).count()

def get_user_count():
  return len(stackblink_results.distinct('email'))

def update_group(id, positions, interesting):
  # TODO add crowdsourced info to group. Update pos_x, pos_y, reviews, score
  pass
