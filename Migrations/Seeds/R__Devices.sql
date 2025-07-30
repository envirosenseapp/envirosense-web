INSERT INTO accounts(id,email, password)
VALUES ('c237acfd-2a83-4390-923f-26bf4e84065c','maxiannicu@gmail.com', '$2a$10$gEvNiCkX81FD/YjTH.RsZePSkJ1P5iDBJ4UVyCs/T09SIY1laNfY6');

INSERT INTO devices (id, name, updated_at,account_id, created_at)
VALUES ('017646c4-66f7-11f0-9a50-be1cb330f7bb', 'Thermometer', '2025-07-20 10:15:30', 'c237acfd-2a83-4390-923f-26bf4e84065c','2025-07-20 10:15:30'),
       ('01765984-66f7-11f0-9a50-be1cb330f7bb', 'Hygrometer', '2025-07-20 13:20:14','c237acfd-2a83-4390-923f-26bf4e84065c', '2025-07-20 12:45:00'),
       ('017659c0-66f7-11f0-9a50-be1cb330f7bb', 'Barometer', '2025-07-20 09:05:22', 'c237acfd-2a83-4390-923f-26bf4e84065c','2025-07-19 08:00:00')
ON CONFLICT (id) DO UPDATE
    SET name       = excluded.name,
        updated_at = excluded.updated_at,
        created_at = excluded.created_at;
