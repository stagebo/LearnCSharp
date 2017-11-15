create table t_ybsUser(uid nvarchar(64),pwd nvarchar(64));
create table t_question(
[name] nvarchar(512),
[ans] nvarchar(512),
[A] nvarchar(2),
[B] nvarchar(2),
[C] nvarchar(2),
[D] nvarchar(2),
[E] nvarchar(2),
[qid] nvarchar(16),
[ana] nvarchar(512));
create table t_answer(
name nvarchar(512),
ans nvarchar(2)
);