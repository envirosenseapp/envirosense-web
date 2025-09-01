create table device_api_keys (
    id uuid not null primary key default uuid_generate_v1(),
    name text not null,
    key_hash text not null,
    device_id uuid not null,
    disabled_at timestamp null,
    created_at timestamp not null default current_timestamp,
    constraint device_api_keys_device_id_fkey
    foreign key (device_id) references devices (id)
    on delete cascade
)
