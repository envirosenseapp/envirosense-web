CREATE TABLE account_password_resets
(
    id            uuid      NOT NULL PRIMARY KEY DEFAULT uuid_generate_v1(),
    account_id    uuid      NOT NULL,
    security_code uuid      NOT NULL,
    used_at       timestamp,
    reset_date    timestamp NOT NULL,
    CONSTRAINT accounts_password_resets_accounts_id_fkey
        FOREIGN KEY (account_id)
            REFERENCES accounts (id)
            ON DELETE CASCADE
);