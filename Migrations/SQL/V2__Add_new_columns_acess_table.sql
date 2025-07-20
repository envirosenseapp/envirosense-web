ALTER TABLE accesses
    ADD COLUMN ip_address VARCHAR,
    ADD COLUMN client VARCHAR,
    ADD COLUMN resource VARCHAR;