alter table `posts`
add column `visits` int not null default 0 after `content`;