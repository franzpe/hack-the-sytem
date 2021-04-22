#!/usr/bin/env bash

# Run the container
docker run --rm -it -p 8080:80 hack-the-system-app $@
