#!/bin/bash

/bin/bash /docker-entrypoint-initdb.d/CuentaAPI/DB_scripts/entrypoint.sh

if [ $? -ne 0 ]; then
    echo "Db Cuenta executed."
    exit 1
fi

/bin/bash /docker-entrypoint-initdb.d/ClienteAPI/DB_scripts/entrypoint.sh

if [ $? -ne 0 ]; then
    echo "Db cliente executed."
    exit 1
fi

exit 0