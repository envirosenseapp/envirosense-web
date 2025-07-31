CREATE
    EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE accesses
(
    id         uuid      NOT NULL PRIMARY KEY DEFAULT uuid_generate_v1(),
    created_at timestamp NOT NULL             DEFAULT current_timestamp
)
