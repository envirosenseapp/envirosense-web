
# envirosense-web


## Prerequisites

A PostgreSQL instance is need in order to run service. To set up for development, you can use built-in docker compose file:
 
```bash
docker compose up -d
```

### SQLFLUFF

`sqlfluff` is a SQL linter which has auto-fix capability. It is used to lint and fix our SQL scripts.

To install tool on `ubuntu`, run:
```bash
pipx install sqlfluff
```

or follow official [instruction](https://docs.sqlfluff.com/en/stable/gettingstarted.html).

## Using Makefile

Before committing changes, run following commands to check code quality(.SQL and .NET):
```
make validate
```

or run following command to automatically fix linter issues(if possible)
```
make format
```