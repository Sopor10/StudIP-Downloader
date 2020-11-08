#!/bin/bash

docker-compose build studipdownloader
docker tag studipdownloader:latest 192.168.0.76:5000/studipdownloader:latest
docker push 192.168.0.76:5000/studipdownloader:latest 
