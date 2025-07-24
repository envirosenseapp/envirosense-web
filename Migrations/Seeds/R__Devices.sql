INSERT INTO devices (id, name, updated_at, created_at)
VALUES ('017646c4-66f7-11f0-9a50-be1cb330f7bb', 'Thermometer', '2025-07-20 10:15:30', '2025-07-20 10:15:30'),
       ('01765984-66f7-11f0-9a50-be1cb330f7bb', 'Hygrometer', '2025-07-20 13:20:14', '2025-07-20 12:45:00'),
       ('017659c0-66f7-11f0-9a50-be1cb330f7bb', 'Barometer', '2025-07-20 09:05:22', '2025-07-19 08:00:00')
ON CONFLICT (id) DO UPDATE
    SET name       = EXCLUDED.name,
        updated_at = EXCLUDED.updated_at,
        created_at = EXCLUDED.created_at;
