create table if not exists posts (
    id int auto_increment primary key,
    title text not null,
    content mediumtext null ,
    create_at datetime(3) not null,
    update_at datetime(3) null
) engine = InnoDB
default charset = utf8mb4;