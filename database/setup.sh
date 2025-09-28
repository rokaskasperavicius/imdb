# !/bin/bash
echo "Running initial setup..."
psql -h cit.ruc.dk -p 5432 -U cit38 -d cit38 -f ../imdb.txt
psql -h cit.ruc.dk -p 5432 -U cit38 -d cit38 -f ../omdb_data.txt
psql -h cit.ruc.dk -p 5432 -U cit38 -d cit38 -f ../wi.txt

echo "Running B2_build_movie_db.sql..."
psql -h cit.ruc.dk -p 5432 -U cit38 -d cit38 -f B2_build_movie_db.sql

echo "Running C2_build_framework_db.sql..."
psql -h cit.ruc.dk -p 5432 -U cit38 -d cit38 -f C2_build_framework_db.sql

echo "Running D2_functionality.sql..."
psql -h cit.ruc.dk -p 5432 -U cit38 -d cit38 -f D2_functionality.sql

echo "Running F2_testing.sql..."
psql -h cit.ruc.dk -p 5432 -U cit38 -d cit38 -f F2_testing.sql > output.txt
