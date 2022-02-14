do
$$
begin
	CREATE TABLE users (
	    id uuid not null ,
	    pseudo varchar not null ,
	    email varchar not null ,
	    password_hash varchar not null ,
	    password_salt varchar not null ,
	    created_at timestamp not null ,
	    created_by uuid not null ,
	    updated_at timestamp not null ,
	    updated_by uuid not null
	);
	ALTER TABLE users ADD CONSTRAINT pk_users PRIMARY KEY ( id );
	CREATE INDEX idx_users_email ON users ( email );
	
	CREATE TABLE histories (
	    id uuid not null ,
	    user_id uuid not null ,
	    rank integer not null ,
	    kills integer not null ,
	    deaths integer not null ,
	    map_id uuid not null ,
	    hero_id uuid not null ,
	    created_at timestamp not null ,
	    created_by uuid not null ,
	    updated_at timestamp not null ,
	    updated_by uuid not null
	);
	ALTER TABLE histories ADD CONSTRAINT pk_histories PRIMARY KEY ( id );
	CREATE INDEX idx_histories_user_id ON histories ( user_id );
end;
$$;