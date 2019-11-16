--------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------PROCEDURE--------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------Register--------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
DROP PROCEDURE Register;
CREATE OR REPLACE PROCEDURE REGISTER (p_username IN USERS."Username"%TYPE, 
                                      p_password IN USERS."Password"%TYPE
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
-------------------------------------------------EXAMPLE Register---------------------------------------------------------------
BEGIN
REGISTER('12345', '12345');
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------Login---------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE LOGIN;
CREATE OR REPLACE PROCEDURE LOGIN (p_username IN USERS."Username"%TYPE, 
                                   p_password IN USERS."Password"%TYPE, 
                                   p_userid OUT USERS."UserID"%TYPE, 
                                   p_addressid OUT ADDRESS."AddressID"%TYPE,
                                   p_customerid OUT CUSTOMER."CustomerID"%TYPE)
IS
BEGIN
  SELECT USERS."UserID", ADDRESS."AddressID", CUSTOMER."CustomerID" INTO p_userid, p_addressid, p_customerid FROM CUSTOMER 
             INNER JOIN ADDRESS ON CUSTOMER."AddressID" = ADDRESS."AddressID" 
             INNER JOIN USERS ON CUSTOMER."UserID" = USERS."UserID" 
    WHERE USERS."Username" = p_username AND USERS."Password" = p_password;
END LOGIN;
--------------------------------------------------EXAMPLE Login-----------------------------------------------------------------
DECLARE
userid USERS."UserID"%TYPE;
addressid ADDRESS."AddressID"%TYPE;
customerid CUSTOMER."CustomerID"%TYPE;
BEGIN
LOGIN('Nikita','123456', userid, addressid, customerid);
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line(userid || ' ' || addressid || ' ' || customerid);
END;
--------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------PersonalInfoSelect-----------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE PERSONALINFOSELECT;
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
-------------------------------------------------EXAMPLE PersonalInfoSelect-----------------------------------------------------
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
CREATE OR REPLACE PROCEDURE PERSONAINFOUPDATE(p_userid IN USERS."UserID"%TYPE,
                                              p_username IN USERS."Username"%TYPE, 
                                              p_password IN USERS."Password"%TYPE,
                                              p_addressid IN ADDRESS."AddressID"%TYPE, 
                                              p_city IN ADDRESS."City"%TYPE, 
                                              p_street IN ADDRESS."Street"%TYPE, 
                                              p_housenumber IN ADDRESS."HouseNumber"%TYPE, 
                                              p_room IN ADDRESS."Room"%TYPE, 
                                              p_firstname IN CUSTOMER."FirstName"%TYPE, 
                                              p_secondname IN CUSTOMER."SecondName"%TYPE, 
                                              p_patronymic IN CUSTOMER."Patronymic"%TYPE, 
                                              p_dateofbirth IN CUSTOMER."DateOfBirth"%TYPE,
                                              p_email IN CUSTOMER."Email"%TYPE, 
                                              p_phonenumber IN CUSTOMER."PhoneNumber"%TYPE
                                              ) IS
BEGIN
UPDATE USERS SET USERS."Username" = p_username, USERS."Password" = p_password 
             WHERE USERS."UserID" = p_userid;
UPDATE ADDRESS SET ADDRESS."City" = p_city, ADDRESS."Street" = p_street, ADDRESS."HouseNumber" = p_housenumber, 
                   ADDRESS."Room" = p_room 
               WHERE ADDRESS."AddressID" = p_addressid;
UPDATE CUSTOMER SET CUSTOMER."FirstName" = p_firstname, CUSTOMER."SecondName" = p_secondname, CUSTOMER."Patronymic" = p_patronymic,
                    CUSTOMER."DateOfBirth" = p_dateofbirth, CUSTOMER."Email" = p_email, CUSTOMER."PhoneNumber" = p_phonenumber
                WHERE CUSTOMER."UserID" = p_userid AND CUSTOMER."AddressID" = p_addressid; 
COMMIT;
END;
DECLARE
username USERS."Username"%TYPE := 'Olesia';
pass USERS."Password"%TYPE := 'password';
city ADDRESS."City"%TYPE := 'Minsk';
street ADDRESS."Street"%TYPE := 'Beloruskaya';
housenumber ADDRESS."HouseNumber"%TYPE := '12';
room ADDRESS."Room"%TYPE := '1';
firstname CUSTOMER."FirstName"%TYPE := 'Olesia';
secondname CUSTOMER."SecondName"%TYPE := 'Mileshko';
patronymic CUSTOMER."Patronymic"%TYPE := 'Hz';
dateofbirth date := '02.04.2000';
email CUSTOMER."Email"%TYPE := 'mil_lesya@mail.ru';
phonenumber CUSTOMER."PhoneNumber"%TYPE := '375333365144';
-------------------------------------------------EXAMPLE PersonalInfoUpdate-----------------------------------------------------
BEGIN
PERSONAINFOUPDATE(77,username, pass, 95, city, street, housenumber, room, firstname, secondname, patronymic, dateofbirth, email, 
                  phonenumber);
DBMS_OUTPUT.put_line(username || pass|| ' ' || pass || ' ' || city || ' ' || street || ' ' || housenumber || ' ' || room ||
                     ' ' || firstname || ' ' || secondname || ' ' || patronymic || ' ' || dateofbirth|| ' ' || email ||
                     ' ' || phonenumber);
END;         
--------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------ShowCatalog--------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE SHOWCATALOG(p_productid OUT "PRODUCT"."ProductID"%TYPE, p_name OUT "PRODUCT"."Name"%TYPE,
                                        p_manufacturer OUT "PRODUCT"."Manufacturer"%TYPE, p_price OUT "PRODUCT"."Price"%TYPE,
                                        p_photo OUT "PRODUCT"."Photo"%TYPE) IS
BEGIN
FOR rec IN (
SELECT PRODUCT."ProductID", PRODUCT."Name", PRODUCT."Manufacturer", PRODUCT."Price", PRODUCT."Photo"
INTO p_productid, p_name, p_manufacturer, p_price, p_photo
FROM PRODUCT)
LOOP
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line( rec."ProductID" || rec."Name" || rec."Manufacturer" || rec."Price");
END LOOP;
END SHOWCATALOG;
-------------------------------------------------EXAMPLE SHOWCATALOG------------------------------------------------------------
DECLARE
productid "PRODUCT"."ProductID"%TYPE;
name "PRODUCT"."Name"%TYPE;
manufacturer "PRODUCT"."Manufacturer"%TYPE;
price "PRODUCT"."Price"%TYPE;
photo "PRODUCT"."Photo"%TYPE;
BEGIN
SHOWCATALOG(productid, name, manufacturer, price, photo);
END;
--------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------Total Price Basket------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE TOTALPRICEBASKET(p_customer IN "BASKET"."CustomerID"%TYPE,
                                             p_total OUT INTEGER) IS
BEGIN
    SELECT SUM((BASKET."Price")) 
        INTO p_total 
            FROM BASKET 
    WHERE BASKET."CustomerID" = p_customer;
END TOTALPRICEBASKET;
-------------------------------------------------EXAMPLE Total Price Basket-----------------------------------------------------
DECLARE
total INTEGER;
BEGIN
TOTALPRICEBASKET(29, total);
DBMS_OUTPUT.put_line(total);
END;
--------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------Rating Product-----------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE RATINGPRODUCT(p_product IN "REVIEW"."ProductID"%TYPE,
                                             p_rating OUT INT) IS                                            
BEGIN
    SELECT CEIL(AVG(REVIEW."Rating")) INTO p_rating FROM REVIEW WHERE REVIEW."ProductID" = p_product; 
END;
-------------------------------------------------EXAMPLE Rating Product---------------------------------------------------------
DECLARE 
rating INT;
BEGIN
RATINGPRODUCT(21, rating);
DBMS_OUTPUT.put_line(rating);
END;
--------------------------------------------------------------------------------------------------------------------------------

SELECT DISTINCT --"PRODUCT"."ProductID","PRODUCT"."Name", "PRODUCT"."Manufacturer", "PRODUCT"."Price", "PRODUCT"."Photo", 
        "OPTIONTYPE"."OptionTypeName", 
        "OPTION"."OptionName", "PRODUCTOPTION"."Value" 
        FROM APPUSER.PRODUCT 
        INNER JOIN APPUSER.PRODUCTOPTION ON APPUSER.PRODUCT."ProductID" = APPUSER.PRODUCTOPTION."ProductID"
        INNER JOIN APPUSER."OPTION" ON APPUSER."OPTION"."OptionID" = APPUSER."PRODUCTOPTION"."OptionID"
        INNER JOIN APPUSER."OPTIONTYPE" ON APPUSER."OPTIONTYPE"."OptionTypeID" = APPUSER."OPTION"."OptionTypeID" WHERE "PRODUCT"."ProductID" = 1 
      
SELECT LISTAGG("PRODUCTOPTION"."Value",';') WITHIN GROUP (ORDER BY  "OPTION"."OptionID" ASC), LISTAGG("OPTION"."OptionName", ';')  WITHIN GROUP (ORDER BY "OPTION"."OptionID" ASC) FROM APPUSER."PRODUCTOPTION"
INNER JOIN APPUSER."OPTION" ON APPUSER."OPTION"."OptionID" = APPUSER."PRODUCTOPTION"."OptionID" WHERE   "ProductID" = 1 
SELECT "PRODUCTOPTION"."Value" FROM APPUSER."PRODUCTOPTION" WHERE "ProductID" = 1 Order by "OptionID" DESC;

SELECT PRODUCT."Price", BASKET."Amount" FROM BASKET INNER JOIN PRODUCT on BASKET."ProductID" = PRODUCT."ProductID" WHERE BASKET."CustomerID" = 29;


                      