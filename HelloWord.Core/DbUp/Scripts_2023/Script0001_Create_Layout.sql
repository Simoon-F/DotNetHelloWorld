create table if not exists posts (
    id int auto_increment primary key,
    title text not null,
    content mediumtext null ,
    create_at int not null,
    update_at int null
) engine = InnoDB
default charset = utf8mb4;