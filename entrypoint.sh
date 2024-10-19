#!/bin/bash

/bin/bash /docker-entrypoint-initdb.d/AccountApp.Data/Db/entrypoint.sh

if [ $? -ne 0 ]; then
    echo "Db Account executed."
    exit 1
fi

/bin/bash /docker-entrypoint-initdb.d/ClientApp.Data/Db/entrypoint.sh

if [ $? -ne 0 ]; then
    echo "Db client executed."
    exit 1
fi

exit 0