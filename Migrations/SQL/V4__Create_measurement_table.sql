CREATE TABLE measurements
(
    id         uuid      NOT NULL primary key DEFAULT uuid_generate_v1(),
    temperature FLOAT,
    humidity FLOAT,
    recording_date timestamp NOT NULL,
    created_at timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    device_id uuid NOT NULL,
        CONSTRAINT measurements_device_id_fkey
        FOREIGN KEY (device_id)
        REFERENCES devices(id)
        ON DELETE CASCADE
)