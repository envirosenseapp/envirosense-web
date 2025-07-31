ALTER TABLE accesses
    ADD COLUMN account_id uuid,
    ADD CONSTRAINT accesses_account_id_fkey
        FOREIGN KEY (account_id)
            REFERENCES accounts (id)
            ON DELETE CASCADE;