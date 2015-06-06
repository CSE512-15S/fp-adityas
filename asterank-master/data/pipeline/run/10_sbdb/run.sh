#!/bin/bash -e

# Boilerplate
cd `dirname $0`
OUT="`git rev-parse --show-toplevel`/data/pipeline/out"
STATIC="`git rev-parse --show-toplevel`/data/pipeline/static"
mkdir -p $OUT/sbdb

# Run sbdb calculations
python horizon.py populateDb \
  --data_path="$STATIC/latest_sbdb.csv" \
  --mass_path="$STATIC/masses.txt" \
  --dv_path="$OUT/deltav/computed_dv.csv"
