ALTER TABLE devices
    ADD COLUMN account_id uuid;

UPDATE devices
SET account_id = (SELECT id
                  FROM accounts
                  ORDER BY created_at ASC
                  LIMIT 1)
WHERE account_id IS NULL;

ALTER TABLE devices
    ALTER COLUMN account_id SET NOT NULL,
    ADD CONSTRAINT devices_account_id_fkey
        FOREIGN KEY (account_id)
            REFERENCES accounts (id)
            ON DELETE cascade;