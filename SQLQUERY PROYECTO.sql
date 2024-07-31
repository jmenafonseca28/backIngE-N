use ingenieriaeyn;


CREATE TABLE Userr (
    id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(512) NOT NULL,
    token VARCHAR(512) NULL,
    role VARCHAR(255) NOT NULL
);

--Agregale unique a email
ALTER TABLE Userr ADD CONSTRAINT UQ_Userr_Email UNIQUE (email);

CREATE TABLE PlayList (
    id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    name VARCHAR(255) NOT NULL,
    user_id uniqueidentifier NOT NULL,
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES Userr(id)
);

CREATE TABLE Channel (
    id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    title VARCHAR(255) NOT NULL,
    url VARCHAR(255) NOT NULL,
    tvg_id VARCHAR(255) NULL,
    tvg_name VARCHAR(255) NULL,
    tvg_channel_number int NULL,
    logo VARCHAR(255) NULL,
    group_title VARCHAR(255) NULL,
    play_list_id uniqueidentifier NULL,
    order_list int not NULL DEFAULT 0,
    state bit NOT NULL DEFAULT 1,
    id_group uniqueidentifier NULL,
    CONSTRAINT fk_group FOREIGN KEY (id_group) REFERENCES Groups(id),
    CONSTRAINT fk_playlist FOREIGN KEY (play_list_id) REFERENCES PlayList(id) on delete cascade
);

/*CREATE TABLE Groups(
    id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    id_playlist uniqueidentifier NOT NULL,
    name VARCHAR(255) NOT NULL,
    CONSTRAINT fk_playlist FOREIGN KEY (id_playlist) REFERENCES PlayList(id) on delete cascade
);*/


/*CREATE TABLE Channel_PlayList (
    channel_id uniqueidentifier NOT NULL,
    playlist_id uniqueidentifier NOT NULL,
   CONSTRAINT pk_channel_playlist PRIMARY KEY (channel_id, playlist_id),
    CONSTRAINT fk_channel FOREIGN KEY (channel_id) REFERENCES Channel(id) on delete cascade,
    CONSTRAINT fk_playlist FOREIGN KEY (playlist_id) REFERENCES PlayList(id) on delete cascade
);*/

CREATE TABLE Security(
    id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    ip VARCHAR(255) NOT NULL,
    login_time DATETIME NULL,
    status_login bit not NULL,
);

CREATE TABLE BlockedIP(
    id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    ip VARCHAR(255) NOT NULL,
    block_time DATETIME NULL,
);

/* CREATE TRIGGER TR_DISORDER 
ON Channel 
AFTER UPDATE 
AS 
BEGIN
    IF UPDATE(state) BEGIN
        UPDATE Channel SET order_list = 0 WHERE id IN (SELECT id FROM deleted WHERE state = 0);
    END;
END; */




SELECT * from [Security];
SELECT * from [BlockedIP];
select * from Userr;
SELECT * from PlayList;
SELECT * from Channel;
--SELECT * from Channel_PlayList;
--SELECT * from Groups;

--alter TABLE Channel drop column id_group;

--drop TABLE Groups;

delete from [Security] where ip = '::1';
delete from [Security] where status_login = '0';
DELETE from BlockedIP where not ip = '::1';
delete from Userr where id = 'adae855a-3744-4fdc-8218-f6d1af993bfc';
delete from PlayList where id = '23fd698f-3c96-4c35-8dda-f54e40134ce2';
delete from Channel where not id = '23fd698f-3c96-4c35-8dda-f54e40134ce2';

update Channel set url = 'https://alba-cr-repretel-c6.stream.mediatiquestream.com/480p.m3u8' where id= '71c82815-c760-4514-8cd5-5e54cf0d5e2b'

--delete from Channel where order_list = 0;
--delete from Channel_PlayList where playlist_id = '48e169f4-61fa-4ef6-8e02-675ee651d507';


--SELECT * from Channel_PlayList;

INSERT INTO Userr (name, last_name, email, password, role) VALUES ('Elias', 'Mena', 'elias@gmail.com', '1234', 'admin');

ALTER TABLE Userr
ADD CONSTRAINT UQ_Userr_Email UNIQUE (email);


update PlayList set name = 'No eliminar' where id = '951dfa78-f80b-4635-b717-5b5675a4f4a2';

update Userr set password = '$2a$11$NgQEa2JrzozrDn7LNezhbetDDBzY09vOGC0hk7c51PtQ/3O4e1Plq' where id = 'd7c35a84-4980-49b8-a66d-5c2fe0df5638';

INSERT INTO PlayList (name, user_id) VALUES ('Lista de prueba', 'd7c35a84-4980-49b8-a66d-5c2fe0df5638');

update Channel set state = 1 where id = 'ae517f77-3a7a-4bd2-8683-bc5b68410ccd';

update Channel set order_list = 1 where id = '163debbd-a1e6-430e-b91b-b415d604b2c5';--Canal 4
update Channel set order_list = 2 where id = 'dd82aa94-2567-4257-8038-3a2e258092b0';--Canal 11
update Channel set order_list = 3 where id = '95ea9a9d-b061-4ba3-86f6-192e33c981d1';--Canal 8
update Channel set order_list = 4 where id = '1dea7a09-d4ac-427a-bc8d-631eda19855a';--Canal 1
UPDATE Channel set order_list = 5 where id = '9635ed56-a9bf-412e-aab6-e1b17dbd4993';--Canal 6
UPDATE Channel set order_list = 6 where id = 'a3dc95d1-e65f-4233-9006-ddfd1d25f902';--Canal 2


INSERT INTO Channel (title, url, tvg_id, tvg_name, tvg_channel_number, logo, group_title) 
VALUES ('Canal 1 CR (720p)', 'https://59ef525c24caa.streamlock.net/canal1cr/canal1cr/playlist.m3u8', 'Canal1.cr', NULL, 1, 'https://i.ibb.co/KhNS8Dy/cropped-logoo-1.png', 'General');

INSERT INTO Channel (title, url, tvg_id, tvg_name, tvg_channel_number, logo, group_title) 
VALUES ('Canal 2 CR (576p)', 'https://alba-cr-repretel-c2.stream.mediatiquestream.com/index.m3u8', 'Canal2.cr', 'Canal2.cr', 2, 'https://i.imgur.com/Nn8BtH5.png', 'General');

INSERT INTO Channel (title, url, tvg_id, tvg_name, tvg_channel_number, logo, group_title) 
VALUES ('Canal 4 CR (720p)', 'https://alba-cr-repretel-c4.stream.mediatiquestream.com/index.m3u8', 'Canal4.cr', NULL, 4, 'https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Repretel_4_logo.png/150px-Repretel_4_logo.png', 'General');

INSERT INTO Channel (title, url, tvg_id, tvg_name, tvg_channel_number, logo, group_title) 
VALUES ('Canal 6 CR (720p)', 'https://alba-cr-repretel-c6.stream.mediatiquestream.com/tracks-v3a1/mono.m3u8', 'Canal6.cr', NULL, 6, 'https://i.imgur.com/nfKJftW.png', 'General');

INSERT INTO Channel (title, url, tvg_id, tvg_name, tvg_channel_number, logo, group_title)
VALUES ('Canal 8 CR (720p)', 'https://mdstrm.com/live-stream-playlist/5a7b1e63a8da282c34d65445.m3u8', 'Canal8.cr', NULL, 8, 'https://i.imgur.com/nefPi2Y.png', 'General');


INSERT INTO Channel_PlayList (channel_id, playlist_id) VALUES ('638c45ae-fe5e-46e1-b711-21ee1515477d', '951dfa78-f80b-4635-b717-5b5675a4f4a2');
INSERT INTO Channel_PlayList (channel_id, playlist_id) VALUES ('ceb09225-309b-48c1-8c9d-362c90c1d8e8', '951dfa78-f80b-4635-b717-5b5675a4f4a2');
INSERT INTO Channel_PlayList (channel_id, playlist_id) VALUES ('ad4db360-be07-498c-9e23-3ecf5eedc026', '951dfa78-f80b-4635-b717-5b5675a4f4a2');
INSERT INTO Channel_PlayList (channel_id, playlist_id) VALUES ('71c82815-c760-4514-8cd5-5e54cf0d5e2b', '951dfa78-f80b-4635-b717-5b5675a4f4a2');
INSERT INTO Channel_PlayList (channel_id, playlist_id) VALUES ('ff5d67cb-1895-4ddc-822f-bb20660ccad3', '951dfa78-f80b-4635-b717-5b5675a4f4a2');