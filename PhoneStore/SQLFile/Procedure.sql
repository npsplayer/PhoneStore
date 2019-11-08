--------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------PROCEDURE--------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------------Register--------------------------------------------------------------------
DROP PROCEDURE Register;
CREATE OR REPLACE PROCEDURE REGISTER (p_username IN USERS."Username"%TYPE, 
                                      p_password IN USERS."Password"%TYPE--,
                                      --p_city IN ADDRESS."City"%TYPE, 
                                      --p_street IN ADDRESS."Street"%TYPE, 
                                      --p_housenumber IN ADDRESS."HouseNumber"%TYPE, 
                                      --p_room IN ADDRESS."Room"%TYPE, 
                                      --p_firstname IN CUSTOMER."FirstName"%TYPE, 
                                      --p_secondname IN CUSTOMER."SecondName"%TYPE, 
                                      --p_patronymic IN CUSTOMER."Patronymic"%TYPE, 
                                      --p_dateofbirth IN CUSTOMER."DateOfBirth"%TYPE,
                                      --p_email IN CUSTOMER."Email"%TYPE, 
                                      --p_phonenumber IN CUSTOMER."PhoneNumber"%TYPE
                                      ) IS
BEGIN
    INSERT INTO USERS("Username", "Password") 
        VALUES (p_username, p_password);
    INSERT INTO ADDRESS("AddressID") 
        VALUES (SQ_ADDRESS.NEXTVAL);
    INSERT INTO CUSTOMER("CustomerID","AddressID","UserID", "DateOfBirth")
        VALUES (SQ_CUSTOMER.nextval,SQ_ADDRESS.CURRVAL, SQ_USERS.CURRVAL, '01.01.1900');
        COMMIT;
END;
BEGIN
REGISTER('12345', '12345');
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------Login---------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
CREATE OR REPLACE PROCEDURE LOGIN (p_username IN USERS."Username"%TYPE, 
                                      p_password IN USERS."Password"%TYPE , p_userid OUT USERS."UserID"%TYPE)
IS
BEGIN
  SELECT "UserID" INTO p_userid FROM "USERS" WHERE "Username" = p_username and "Password" = p_password;
END LOGIN;
DECLARE
id USERS."UserID"%TYPE;
BEGIN
spGEtUser('Nikita','123456', id);
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line(id);
END;
--------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------PersonalInfoSelect-----------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
CREATE OR REPLACE PROCEDURE PERSONALINFOSELECT (p_userid IN USERS."UserID"%TYPE,
                                                p_username OUT USERS."Username"%TYPE, 
                                                p_password OUT USERS."Password"%TYPE,
                                                p_city OUT ADDRESS."City"%TYPE, 
                                                p_street OUT ADDRESS."Street"%TYPE, 
                                                p_housenumber OUT ADDRESS."HouseNumber"%TYPE, 
                                                p_room OUT ADDRESS."Room"%TYPE, 
                                                p_firstname OUT CUSTOMER."FirstName"%TYPE, 
                                                p_secondname OUT CUSTOMER."SecondName"%TYPE, 
                                                p_patronymic OUT CUSTOMER."Patronymic"%TYPE, 
                                                p_dateofbirth OUT CUSTOMER."DateOfBirth"%TYPE,
                                                p_email OUT CUSTOMER."Email"%TYPE, 
                                                p_phonenumber OUT CUSTOMER."PhoneNumber"%TYPE
                                                ) IS
BEGIN
    SELECT USERS."Username", USERS."Password", ADDRESS."City", ADDRESS."Street", ADDRESS."HouseNumber", ADDRESS."Room", 
           CUSTOMER."FirstName", CUSTOMER."SecondName", CUSTOMER."Patronymic", CUSTOMER."DateOfBirth", CUSTOMER."Email",
           CUSTOMER."PhoneNumber"
    INTO p_username, p_password, p_city, p_street, p_housenumber, p_room, p_firstname, p_secondname, p_patronymic, p_dateofbirth,
         p_email, p_phonenumber
    FROM CUSTOMER 
             INNER JOIN ADDRESS ON CUSTOMER."AddressID" = ADDRESS."AddressID" 
             INNER JOIN USERS ON CUSTOMER."UserID" = USERS."UserID" 
    WHERE USERS."UserID" = p_userid;
END PERSONALINFOSELECT;
DECLARE
username USERS."Username"%TYPE;
pass USERS."Password"%TYPE;
city ADDRESS."City"%TYPE;
street ADDRESS."Street"%TYPE;
housenumber ADDRESS."HouseNumber"%TYPE;
room ADDRESS."Room"%TYPE;
firstname CUSTOMER."FirstName"%TYPE;
secondname CUSTOMER."SecondName"%TYPE;
patronymic CUSTOMER."Patronymic"%TYPE;
dateofbirth date;
email CUSTOMER."Email"%TYPE;
phonenumber CUSTOMER."PhoneNumber"%TYPE;
BEGIN
PERSONALINFOSELECT(26, username, pass, city, street, housenumber, room, firstname, secondname, patronymic, dateofbirth, 
                   email, phonenumber);
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line(username || pass|| ' ' || pass || ' ' || city || ' ' || street || ' ' || housenumber || ' ' || room ||
                     ' ' || firstname || ' ' || secondname || ' ' || patronymic || ' ' || dateofbirth|| ' ' || email ||
                     ' ' || phonenumber);
END;
--------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------PersonalInfoUpdate-----------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
       