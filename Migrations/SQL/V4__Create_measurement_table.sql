CREATE TABLE measurements
(
    id uuid NOT NULL PRIMARY KEY DEFAULT uuid_generate_v1(),
    temperature float,
    humidity float,
    recording_date timestamp NOT NULL,
    created_at timestamp NOT NULL DEFAULT current_timestamp,
    device_id uuid NOT NULL,
    CONSTRAINT measurements_device_id_fkey
    FOREIGN KEY (device_id)
    REFERENCES devices (id)
    ON DELETE CASCADE
)
