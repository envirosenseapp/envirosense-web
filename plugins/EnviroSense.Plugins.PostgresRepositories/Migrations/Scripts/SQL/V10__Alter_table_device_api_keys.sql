ALTER TABLE device_api_keys
RENAME TO api_Keys;
ALTER TABLE api_keys DROP CONSTRAINT device_api_keys_device_id_fkey;
ALTER TABLE api_keys RENAME COLUMN device_id TO account_id;
ALTER TABLE api_keys
    ADD CONSTRAINT api_keys_account_id_fkey
        FOREIGN KEY (account_id) REFERENCES accounts(id)
            ON DELETE CASCADE;