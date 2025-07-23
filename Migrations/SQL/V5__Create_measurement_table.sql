CREATE TABLE measurements
(
    id         uuid      NOT NULL primary key DEFAULT uuid_generate_v1(),
    temperature VARCHAR,
    humidity VARCHAR,
    recording_date timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    recording_created_at timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    device_id uuid NOT NULL,
        CONSTRAINT fk_device
        FOREIGN KEY (device_id)
        REFERENCES devices(id)
        ON DELETE CASCADE
)