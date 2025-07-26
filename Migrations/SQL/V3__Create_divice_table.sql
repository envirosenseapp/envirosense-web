CREATE TABLE devices
(
    id         uuid      NOT NULL primary key DEFAULT uuid_generate_v1(),
    name       VARCHAR,
    updated_at timestamp NOT NULL             DEFAULT CURRENT_TIMESTAMP,
    created_at timestamp NOT NULL             DEFAULT CURRENT_TIMESTAMP
)