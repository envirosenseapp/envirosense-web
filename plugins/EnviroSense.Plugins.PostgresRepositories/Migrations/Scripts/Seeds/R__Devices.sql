--- Accounts ---

INSERT INTO accounts (id, email, password)
VALUES (
    'c237acfd-2a83-4390-923f-26bf4e84065c',
    'maxiannicu@gmail.com',
    '$2a$10$gEvNiCkX81FD/YjTH.RsZePSkJ1P5iDBJ4UVyCs/T09SIY1laNfY6'
),
(
    '0198c711-99a7-7377-b70b-9753db34a1b3',
    'maxian.sandu@gmail.com',
    '$2a$10$apu432Z/Cn5.SkNWQsnxOOiLoaiqECDCW47OnyGr0nLr.6.Bpta.G'
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
),
(
    '0198c711-99a7-7377-b70b-9753db34a1b3',
    'Garage thermometer',
    '2025-08-21 01:16:35',
    '0198c711-99a7-7377-b70b-9753db34a1b3',
    '2025-08-21 22:22:22'

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
),
-- Garage thermometer measurements ('0198c711-99a7-7377-b70b-9753db34a1b3')

(
    '7a1f2e10-12b4-45c2-b87a-11f0a1cde201',
    19.2,
    60,
    '2025-08-18 00:42:00',
    '2025-08-18 00:42:05.432',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '0b8a77e5-34c1-4d4e-9109-90b3e1fd22a7',
    18.9,
    62,
    '2025-08-18 02:15:00',
    '2025-08-18 02:15:06.981',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '34f92167-890c-4d9c-912d-75b83188b2c0',
    20.1,
    58,
    '2025-08-18 04:05:00',
    '2025-08-18 04:05:03.224',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '8c7c23fb-1bde-41e3-9019-b62a4d6b8479',
    21.0,
    55,
    '2025-08-18 06:28:00',
    '2025-08-18 06:28:04.672',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '5d67f4a9-2c8d-4c32-9fb3-8df1c6aaad34',
    21.8,
    53,
    '2025-08-18 07:50:00',
    '2025-08-18 07:50:05.310',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    'a812d0cb-7c2b-40ad-8341-675f552be9d1',
    22.3,
    51,
    '2025-08-18 08:17:00',
    '2025-08-18 08:17:06.029',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '31c9d0a7-6d88-476e-9438-301d567a88f5',
    23.0,
    50,
    '2025-08-18 09:43:00',
    '2025-08-18 09:43:04.900',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '91f27e5a-8e1d-4d79-89a3-fb2a60d877de',
    23.5,
    49,
    '2025-08-18 10:12:00',
    '2025-08-18 10:12:05.118',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '6a3cbb50-9f2a-462a-bf63-dc8cf7ea2107',
    24.2,
    48,
    '2025-08-18 10:55:00',
    '2025-08-18 10:55:06.401',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    'ad2e3407-6e88-41b0-bf40-26c3cf3f2c7c',
    25.0,
    47,
    '2025-08-18 12:30:00',
    '2025-08-18 12:30:05.022',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    'cf9b8611-fb12-4f80-b68e-8499a4c82cdb',
    25.4,
    46,
    '2025-08-18 12:59:00',
    '2025-08-18 12:59:06.714',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '7fb9a10b-44d7-4b49-924e-167f0aab03dd',
    26.1,
    45,
    '2025-08-18 13:47:00',
    '2025-08-18 13:47:05.882',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    'f1ac4b65-8c4e-45d8-8a32-06b0cbd4f8c2',
    26.8,
    44,
    '2025-08-18 14:20:00',
    '2025-08-18 14:20:06.537',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    'ef90298d-2c9d-4050-9a11-81672df33e21',
    27.2,
    43,
    '2025-08-18 15:02:00',
    '2025-08-18 15:02:05.693',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    'b70f4a23-d19b-4b8a-9645-2d4cb81d12de',
    26.5,
    44,
    '2025-08-18 16:38:00',
    '2025-08-18 16:38:04.927',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    'c62e96a1-bbe7-40ff-a629-5a1fa93db6e2',
    25.9,
    46,
    '2025-08-18 17:55:00',
    '2025-08-18 17:55:06.331',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '77cdb2cb-41d8-4f19-8333-422f0a0cf56a',
    24.7,
    49,
    '2025-08-18 19:22:00',
    '2025-08-18 19:22:05.208',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '0df028ce-61e3-46b2-b22f-5b28c32df131',
    23.8,
    51,
    '2025-08-18 20:05:00',
    '2025-08-18 20:05:06.482',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    'd9a379a7-46d4-4d80-9a7b-10c8334449fa',
    22.4,
    54,
    '2025-08-18 21:41:00',
    '2025-08-18 21:41:05.119',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
),
(
    '25e0181a-2056-48de-92a1-3d8d307ec9e7',
    21.1,
    57,
    '2025-08-18 23:18:00',
    '2025-08-18 23:18:06.708',
    '0198c711-99a7-7377-b70b-9753db34a1b3'
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
    --- wk/9tQ+d5RagmA6ygG3ol7iSwV1e1TAB9CgY0aQh4/s= ---
    '828db438-20f1-49e8-8187-261c6a599ec8',
    'Outside thermometer Device API Key',
    'wk/9tQ+d5RagmA6ygG3ol7iSwV1e1TAB9CgY0aQh4/s=',
    '1be27889-adcc-49a5-b686-00f2b09c8510',
    null,
    '2025-08-18 20:28:27.231119'
), (
    --- Living thermometer API Key ---
    --- 9pvz3Zd6lRNRoV3mdLMyj7LJt2SFYbLt6OB5RE5f2fM= ---
    'ab2f2abc-0de5-4fe2-b74c-9a664e6fd680',
    'Living room thermometer API Key',
    '9pvz3Zd6lRNRoV3mdLMyj7LJt2SFYbLt6OB5RE5f2fM=',
    '01765984-66f7-11f0-9a50-be1cb330f7bb',
    null,
    '2025-08-18 20:34:14.592456'

),
(
    --- Kitchen thermometer API Key ---
    --- STuZPEyRaO5nQ482B0bcZLzlxaf8SCHMN+xhnGzGZx8= ---
    '36efe7b1-a5fe-4206-a4be-bcfeac92662b',
    'Kitchen thermometer API Key',
    'STuZPEyRaO5nQ482B0bcZLzlxaf8SCHMN+xhnGzGZx8=',
    '017646c4-66f7-11f0-9a50-be1cb330f7bb',
    null,
    '2025-08-18 20:35:27.604062'
),
(
    --- Bedroom thermometer API Key ---
    --- gsvGE5Cf+NOI1pt8YGV4YD6XSHW/4XbSFUPEcCAp7Fk= ---
    'ca64ad9d-398b-472f-a7bf-250e24a28b71',
    'Bedroom thermometer API Key',
    'gsvGE5Cf+NOI1pt8YGV4YD6XSHW/4XbSFUPEcCAp7Fk=',
    '017659c0-66f7-11f0-9a50-be1cb330f7bb',
    null,
    '2025-08-18 20:36:30.640857'
)

ON CONFLICT (id) DO UPDATE
    SET
        name = excluded.name,
        key_hash = excluded.key_hash,
        device_id = excluded.device_id,
        disabled_at = excluded.disabled_at,
        created_at = excluded.created_at;
