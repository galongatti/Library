
# Setting development envoriment to run the application locally

## (Optional) Setting development environment with postgresql using docker container
1) Run docker pull postgres:15.15-trixie

2) Use the following command to create and start a Postgres container for local development:
```bash
docker run --name db-pg -e POSTGRES_PASSWORD=123456789 -v db-pg-vol:/var/lib/postgresql/data -p 5432:5432 -d postgres:15.15-trixie
```
Explanation of the options:
- --name db-pg - assign a name to the container.
- -e POSTGRES_PASSWORD=123456789 - set the default Postgres user password.
- -v db-pg-vol:/var/lib/postgresql/data - mount a named volume to persist database files.
- -p 5432:5432 - map host port 5432 to container port 5432.
- -d - run the container in detached mode.

3) If container already exist, use the following command:
```bash
docker start db-pg
```

## Setting db connection secrets for development environment:
1) Run the following command to init the secret dotnet repository
```bash 
dotnet user-secrets init
```

2) Run the following commands to set the database connection secrets
```bash
dotnet user-secrets set "ConnectionStrings:DbConnection" "Host=localhost;Port=5432;Database=Library;Username=postgres;Password=123456789"
```

3) If first time running the application, run the following command to create the database and apply migrations
```bash
dotnet ef database update
```