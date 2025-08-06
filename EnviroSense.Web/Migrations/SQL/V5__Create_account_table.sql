CREATE TABLE accounts
(
    id uuid NOT NULL PRIMARY KEY DEFAULT uuid_generate_v1(),
    email varchar NOT NULL UNIQUE,
    password varchar NOT NULL,
    updated_at timestamp NOT NULL DEFAULT current_timestamp,
    created_at timestamp NOT NULL DEFAULT current_timestamp
);
