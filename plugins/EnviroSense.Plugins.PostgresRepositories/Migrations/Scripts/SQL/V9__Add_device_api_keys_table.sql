create table api_keys (
    id uuid not null primary key default uuid_generate_v1(),
    name text not null,
    key_hash text not null,
    account_id uuid not null,
    disabled_at timestamp null,
    created_at timestamp not null default current_timestamp,
    constraint api_keys_account_id_fkey
    foreign key (account_id) references accounts (id)
    on delete cascade
)
