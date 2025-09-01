ALTER TABLE device_api_keys
RENAME TO api_keys;
ALTER TABLE api_keys ADD COLUMN account_id UUID NULL;
UPDATE api_keys
SET account_id = d.account_id
FROM devices AS d
WHERE api_keys.device_id = d.id;
ALTER TABLE api_keys ALTER COLUMN account_id SET NOT NULL;
ALTER TABLE api_keys
ADD CONSTRAINT api_keys_account_id_fkey
FOREIGN KEY (account_id) REFERENCES accounts (id)
ON DELETE CASCADE;
ALTER TABLE api_keys DROP COLUMN device_id;
