INSERT INTO devices (name, updated_at, created_at)
VALUES 
  ('Thermometer', '2025-07-20 10:15:30', '2025-07-20 10:15:30'),
  ('Hygrometer',  '2025-07-20 13:20:14', '2025-07-20 12:45:00'),
  ('Barometer',   '2025-07-20 09:05:22', '2025-07-19 08:00:00')
RETURNING *;
       