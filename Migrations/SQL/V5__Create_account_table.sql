CREATE TABLE accounts
(
    id         uuid      NOT NULL primary key DEFAULT uuid_generate_v1(),
    email      VARCHAR   NOT NULL UNIQUE,
    password   VARCHAR   NOT NULL,
    updated_at timestamp NOT NULL             DEFAULT CURRENT_TIMESTAMP,
    created_at timestamp NOT NULL             DEFAULT CURRENT_TIMESTAMP
);
