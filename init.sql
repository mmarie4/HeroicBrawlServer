CREATE TABLE users (
    id uuid,
    pseudo varchar,
    email varchar,
    password_hash varchar,
    password_salt varchar,
    created_at timestamp,
    created_by uuid,
    updated_at timestamp,
    updated_by uuid
);
ALTER TABLE users ADD CONSTRAINT pk_users PRIMARY KEY ( id );
CREATE INDEX idx_users_email ON users ( email );

CREATE TABLE histories (
    id uuid,
    user_id uuid,
    rank integer,
    kills integer,
    deaths integer,
    map_id uuid,
    hero_id uuid,
    created_at timestamp,
    created_by uuid,
    updated_at timestamp,
    updated_by uuid
);
ALTER TABLE histories ADD CONSTRAINT pk_histories PRIMARY KEY ( id );
CREATE INDEX idx_histories_user_id ON histories ( user_id );