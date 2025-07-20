CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE accesses
(
    id         uuid      NOT NULL primary key DEFAULT uuid_generate_v1(),
    created_at timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
)