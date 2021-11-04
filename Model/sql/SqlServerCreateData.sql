
USE [practicamad]


INSERT INTO UserProfile
	VALUES('admin', 'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', 'marco', 'polo', 'marcop@udc.es', 'es');

INSERT INTO UserProfile
	VALUES('user', 'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=', 'Ursula', 'Martinez', 'usri_mp@udc.es', 'es');

INSERT INTO Category
	VALUES ('typeTest');

INSERT INTO Photo
	VALUES ('photo1', 'description', GETDATE(), 10, 10, 'iso1', 10, 1, 1);

INSERT INTO Comment
	VALUES('comment1', GETDATE(), 1 , 1);



