CREATE TABLE devices
(
    id         uuid      NOT NULL PRIMARY KEY DEFAULT uuid_generate_v1(),
    name       varchar,
    updated_at timestamp NOT NULL             DEFAULT current_timestamp,
    created_at timestamp NOT NULL             DEFAULT current_timestamp
)
