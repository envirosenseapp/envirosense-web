--- Accounts ---

INSERT INTO accounts (id, email, password)
VALUES (
    'c237acfd-2a83-4390-923f-26bf4e84065c',
    'maxiannicu@gmail.com',
    '$2a$10$gEvNiCkX81FD/YjTH.RsZePSkJ1P5iDBJ4UVyCs/T09SIY1laNfY6'
)
ON CONFLICT (id) DO UPDATE
    SET
        email = excluded.email,
        password = excluded.password;

--- Devices ---

INSERT INTO devices (id, name, updated_at, account_id, created_at)
VALUES (
    '017646c4-66f7-11f0-9a50-be1cb330f7bb',
    'Kitchen thermometer',
    '2025-07-20 10:15:30',
    'c237acfd-2a83-4390-923f-26bf4e84065c',
    '2025-07-20 10:15:30'
),
(
    '01765984-66f7-11f0-9a50-be1cb330f7bb',
    'Living room thermometer',
    '2025-07-20 13:20:14',
    'c237acfd-2a83-4390-923f-26bf4e84065c',
    '2025-07-20 12:45:00'
),
(
    '017659c0-66f7-11f0-9a50-be1cb330f7bb',
    'Bedroom thermometer',
    '2025-07-20 09:05:22',
    'c237acfd-2a83-4390-923f-26bf4e84065c',
    '2025-07-19 08:00:00'
),
(
    '1be27889-adcc-49a5-b686-00f2b09c8510',
    'Outside thermometer',
    '2025-07-20 09:05:22',
    'c237acfd-2a83-4390-923f-26bf4e84065c',
    '2025-08-18 08:00:00'
)
ON CONFLICT (id) DO UPDATE
    SET
        name = excluded.name,
        updated_at = excluded.updated_at,
        created_at = excluded.created_at;

--- Measurements ---

INSERT INTO public.measurements (
    id, temperature, humidity, recording_date, created_at, device_id
)
VALUES
-- Kitchen thermometer measurements ('017646c4-66f7-11f0-9a50-be1cb330f7bb')
(
    'f47ac10b-58cc-4372-a567-0e02b2c3d479',
    22,
    60,
    '2025-07-20 10:20:00',
    '2025-07-20 10:20:05.123',
    '017646c4-66f7-11f0-9a50-be1cb330f7bb'
),
(
    'f47ac10b-58cc-4372-a567-0e02b2c3d480',
    22.5,
    62,
    '2025-07-20 10:25:00',
    '2025-07-20 10:25:03.456',
    '017646c4-66f7-11f0-9a50-be1cb330f7bb'
),

-- Living room thermometer measurements ('01765984-66f7-11f0-9a50-be1cb330f7bb')
(
    'f47ac10b-58cc-4372-a567-0e02b2c3d481',
    24,
    55,
    '2025-07-20 13:25:00',
    '2025-07-20 13:25:04.789',
    '01765984-66f7-11f0-9a50-be1cb330f7bb'
),
(
    'f47ac10b-58cc-4372-a567-0e02b2c3d482',
    24.2,
    54,
    '2025-07-20 13:30:00',
    '2025-07-20 13:30:02.012',
    '01765984-66f7-11f0-9a50-be1cb330f7bb'
),

-- Bedroom thermometer measurements ('017659c0-66f7-11f0-9a50-be1cb330f7bb')
(
    'f47ac10b-58cc-4372-a567-0e02b2c3d483',
    21,
    50,
    '2025-07-20 09:10:00',
    '2025-07-20 09:10:05.345',
    '017659c0-66f7-11f0-9a50-be1cb330f7bb'
),
(
    'f47ac10b-58cc-4372-a567-0e02b2c3d484',
    21.3,
    51,
    '2025-07-20 09:15:00',
    '2025-07-20 09:15:01.678',
    '017659c0-66f7-11f0-9a50-be1cb330f7bb'
),

-- Outside thermometer measurements ('1be27889-adcc-49a5-b686-00f2b09c8510')
(
    '0198bed0-cac1-7200-b23a-41a4c56772ef',
    23,
    51,
    '2025-08-18 20:13:00.000000',
    '2025-08-18 20:13:31.216634',
    '1be27889-adcc-49a5-b686-00f2b09c8510'
),
(
    'f47ac10b-58cc-4372-a567-0e02b2c3d485',
    23.5,
    50,
    '2025-08-18 20:18:00',
    '2025-08-18 20:18:05.901',
    '1be27889-adcc-49a5-b686-00f2b09c8510'
)
ON CONFLICT (id) DO UPDATE
    SET
        temperature = excluded.temperature,
        humidity = excluded.humidity,
        recording_date = excluded.recording_date,
        device_id = excluded.device_id,
        created_at = excluded.created_at;

--- API Keys ---

INSERT INTO public.device_api_keys (
    id, name, key_hash, device_id, disabled_at, created_at
)
VALUES (
    --- Outside thermometer API Key ---
    --- 41ae5fe52f1e4dcabc017b6eaedbd1bc ---
    'b9cf1f88-2838-40b1-9cb4-ecf23e610215',
    'First API key',
    'gr3ko+vvD20PyPJbRNvP11nGt4gCcUJ2zKOxjrPdScY=',
    '1be27889-adcc-49a5-b686-00f2b09c8510',
    null,
    '2025-08-18 20:28:27.231119'
)

ON CONFLICT (id) DO UPDATE
    SET
        name = excluded.name,
        key_hash = excluded.key_hash,
        device_id = excluded.device_id,
        disabled_at = excluded.disabled_at,
        created_at = excluded.created_at;
