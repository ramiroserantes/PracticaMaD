USE [practicamad]

INSERT INTO UserProfile
	VALUES('user', '10/w7o2juYBrGMh32/KbveULW9jk2tejpyUAD+uC6PE=', 'marco', 'm', 'marcop@udc.es', 'ES', 'ES');
	
INSERT INTO UserProfile
	VALUES('anotherUser', '10/w7o2juYBrGMh32/KbveULW9jk2tejpyUAD+uC6PE=', 'pow', 'p', 'pow@udc.es', 'ES', 'ES');

INSERT INTO Category
    VALUES('fuego');

INSERT INTO Category
    VALUES('hielo');

INSERT INTO Photo
    VALUES('fuego', 'desc', GETDATE(), 2, 3, 'iso', 100, '../..Images/1.jpg', 1, 1);
   
INSERT INTO Photo
    VALUES('hielo', 'desc', GETDATE(), 2, 33, 'iso', 1001, '../../Images/2.jpg', 2, 2);

INSERT INTO Photo
    VALUES('hieloAgain', 'desc', GETDATE(), 2, 33, 'iso', 1001, '../../Images/3.jpg', 2, 1);

INSERT INTO Comment 
    VALUES('hola', GETDATE(), 1 , 1);

