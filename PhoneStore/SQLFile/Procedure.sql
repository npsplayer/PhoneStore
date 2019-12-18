--------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------PROCEDURE------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------Register User---------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
DROP PROCEDURE REGISTERUSER;
CREATE OR REPLACE PROCEDURE REGISTERUSER (p_username IN USERS."Username"%TYPE, 
                                      p_password IN USERS."Password"%TYPE
                                      ) IS
BEGIN
    INSERT INTO USERS("Username", "Password", "Role") 
        VALUES (p_username, p_password, 'User');
    INSERT INTO ADDRESS("AddressID") 
        VALUES (SQ_ADDRESS.NEXTVAL);
    INSERT INTO CUSTOMER("CustomerID","AddressID","UserID", "DateOfBirth")
        VALUES (SQ_CUSTOMER.nextval,SQ_ADDRESS.CURRVAL, SQ_USERS.CURRVAL, '01.01.1920');
        COMMIT;
END;
-------------------------------------------------EXAMPLE Register User----------------------------------------------------------
BEGIN
REGISTERUSER('123456', '123456');
END;
--------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------Register Admin---------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
DROP PROCEDURE REGISTEADMIN;
CREATE OR REPLACE PROCEDURE REGISTEADMIN (p_username IN USERS."Username"%TYPE, 
                                          p_password IN USERS."Password"%TYPE
                                          ) IS
BEGIN
    INSERT INTO USERS("Username", "Password", "Role") 
        VALUES (p_username, p_password, 'Admin');
    INSERT INTO ADDRESS("AddressID") 
        VALUES (SQ_ADDRESS.NEXTVAL);
    INSERT INTO CUSTOMER("CustomerID","AddressID","UserID", "DateOfBirth")
        VALUES (SQ_CUSTOMER.nextval,SQ_ADDRESS.CURRVAL, SQ_USERS.CURRVAL, '01.01.1920');
        COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------AdminAddCustomer------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
DROP PROCEDURE ADMINADDCUSTOMER;
CREATE OR REPLACE PROCEDURE ADMINADDCUSTOMER (p_username IN USERS."Username"%TYPE, 
                                      p_password IN USERS."Password"%TYPE,
                                      p_role IN USERS."Role"%TYPE,
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
    INSERT INTO USERS("Username", "Password", "Role") 
        VALUES (p_username, p_password, p_role);
    INSERT INTO ADDRESS("AddressID", "City", "Street", "HouseNumber", "Room") 
        VALUES (SQ_ADDRESS.NEXTVAL, p_city, p_street, p_housenumber, p_room);
    INSERT INTO CUSTOMER("CustomerID","AddressID","UserID", "DateOfBirth", "FirstName", "SecondName", 
                         "Patronymic", "Email", "PhoneNumber")
        VALUES (SQ_CUSTOMER.nextval,SQ_ADDRESS.CURRVAL, SQ_USERS.CURRVAL, p_dateofbirth, p_firstname, p_secondname, 
                p_patronymic, p_email, p_phonenumber);
        COMMIT;
END;
-------------------------------------------------EXAMPLE Admin Add Customer-----------------------------------------------------
BEGIN
ADMINADDCUSTOMER('123456', '123456', 'User', 'qwerty', 'qwerty', '11', '11', 'qwerty','qwerty','qwerty', '02.07.2000', 'qwerty', '123456');
END;
--------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------AdminUpdateCustomer---------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
DROP PROCEDURE ADMINUPDATECUSTOMER;
CREATE OR REPLACE PROCEDURE ADMINUPDATECUSTOMER(p_userid IN USERS."UserID"%TYPE,
                                              p_username IN USERS."Username"%TYPE, 
                                              p_password IN USERS."Password"%TYPE,
                                              p_role IN USERS."Role"%TYPE,
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
UPDATE USERS SET USERS."Username" = p_username, USERS."Password" = p_password, USERS."Role" = p_role
             WHERE USERS."UserID" = p_userid;
UPDATE ADDRESS SET ADDRESS."City" = p_city, ADDRESS."Street" = p_street, ADDRESS."HouseNumber" = p_housenumber, 
                   ADDRESS."Room" = p_room 
               WHERE ADDRESS."AddressID" = p_addressid;
UPDATE CUSTOMER SET CUSTOMER."FirstName" = p_firstname, CUSTOMER."SecondName" = p_secondname, CUSTOMER."Patronymic" = p_patronymic,
                    CUSTOMER."DateOfBirth" = p_dateofbirth, CUSTOMER."Email" = p_email, CUSTOMER."PhoneNumber" = p_phonenumber
                WHERE CUSTOMER."UserID" = p_userid AND CUSTOMER."AddressID" = p_addressid; 
COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------AdminDeleteCustomer---------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
DROP PROCEDURE ADMINDELETECUSTOMER;
CREATE OR REPLACE PROCEDURE ADMINDELETECUSTOMER (p_userid IN USERS."UserID"%TYPE,
                                                 p_addressid IN ADDRESS."AddressID"%TYPE,
                                                 p_customerid IN CUSTOMER."CustomerID"%TYPE
                                                 ) IS
BEGIN                                               
DELETE FROM CUSTOMER WHERE CUSTOMER."CustomerID" = p_customerid;
DELETE FROM USERS WHERE USERS."UserID" = p_userid;
DELETE FROM ADDRESS WHERE ADDRESS."AddressID" = p_addressid;
COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------AdminAddProduct-----------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE ADMINADDPRODUCT;
CREATE OR REPLACE PROCEDURE ADMINADDPRODUCT(p_name IN PRODUCT."Name"%TYPE,
                                            p_manuf IN PRODUCT."Manufacturer"%TYPE,
                                            p_price IN PRODUCT."Price"%TYPE) IS
BEGIN
INSERT INTO PRODUCT("Name", "Manufacturer", "Price") VALUES (p_name, p_manuf, p_price);
COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------AdminUpdateProduct--------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE ADMINUPDATEPRODUCT;
CREATE OR REPLACE PROCEDURE ADMINUPDATEPRODUCT(p_productid IN PRODUCT."ProductID"%TYPE,
                                            p_name IN PRODUCT."Name"%TYPE,
                                            p_manuf IN PRODUCT."Manufacturer"%TYPE,
                                            p_price IN PRODUCT."Price"%TYPE) IS
BEGIN
UPDATE PRODUCT SET PRODUCT."Name" = p_name, PRODUCT."Manufacturer" = p_manuf, PRODUCT."Price" = p_price 
WHERE PRODUCT."ProductID" = p_productid;
COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------AdminDeleteProduct--------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE ADMINDELETEPRODUCT;
CREATE OR REPLACE PROCEDURE ADMINDELETEPRODUCT(p_productid IN PRODUCT."ProductID"%TYPE) IS
BEGIN
DELETE FROM PRODUCT WHERE PRODUCT."ProductID" = p_productid;
COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------AdminAddOption-----------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE ADMINADDOPTION;
CREATE OR REPLACE PROCEDURE ADMINADDOPTION(p_productid IN PRODUCTOPTION."ProductID"%TYPE,
                                           p_optionid IN PRODUCTOPTION."OptionID"%TYPE,
                                           p_value IN PRODUCTOPTION."Value"%TYPE,
                                           p_unit IN PRODUCTOPTION."Unit"%TYPE) IS
BEGIN
INSERT INTO PRODUCTOPTION("ProductID", "OptionID", "Value", "Unit") VALUES (p_productid, p_optionid, p_value, p_unit);
COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------AdminUpdateOption---------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE ADMINUPDATEOPTION;
CREATE OR REPLACE PROCEDURE ADMINUPDATEOPTION(p_productoptionid IN PRODUCTOPTION."ProductOptionID"%TYPE,
                                           p_productid IN PRODUCTOPTION."ProductID"%TYPE,
                                           p_optionid IN PRODUCTOPTION."OptionID"%TYPE,
                                           p_value IN PRODUCTOPTION."Value"%TYPE,
                                           p_unit IN PRODUCTOPTION."Unit"%TYPE) IS
BEGIN
UPDATE PRODUCTOPTION SET PRODUCTOPTION."ProductID" = p_productid, PRODUCTOPTION."OptionID" = p_optionid, 
PRODUCTOPTION."Value" = p_value, PRODUCTOPTION."Unit" = p_unit
WHERE PRODUCTOPTION."ProductOptionID" = p_productoptionid;
COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------AdminDeleteOption---------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE ADMINDELETEOPTION;
CREATE OR REPLACE PROCEDURE ADMINDELETEOPTION(p_productoptionid IN PRODUCTOPTION."ProductOptionID"%TYPE) IS
BEGIN
DELETE FROM PRODUCTOPTION WHERE PRODUCTOPTION."ProductOptionID" = p_productoptionid;
COMMIT;
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------Login---------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------- 
DROP PROCEDURE LOGIN;
CREATE OR REPLACE PROCEDURE LOGIN (p_username IN USERS."Username"%TYPE, 
                                   p_password IN USERS."Password"%TYPE, 
                                   p_userid OUT USERS."UserID"%TYPE, 
                                   p_addressid OUT ADDRESS."AddressID"%TYPE,
                                   p_customerid OUT CUSTOMER."CustomerID"%TYPE,
                                   p_role OUT USERS."Role"%TYPE)
IS
BEGIN
  SELECT USERS."UserID", ADDRESS."AddressID", CUSTOMER."CustomerID", USERS."Role" INTO p_userid, p_addressid, 
             p_customerid, p_role FROM CUSTOMER 
             INNER JOIN ADDRESS ON CUSTOMER."AddressID" = ADDRESS."AddressID" 
             INNER JOIN USERS ON CUSTOMER."UserID" = USERS."UserID" 
    WHERE USERS."Username" = p_username AND USERS."Password" = p_password;
END LOGIN;
--------------------------------------------------EXAMPLE Login-----------------------------------------------------------------
DECLARE
userid USERS."UserID"%TYPE;
roleuser  USERS."Role"%TYPE;
addressid ADDRESS."AddressID"%TYPE;
customerid CUSTOMER."CustomerID"%TYPE;
BEGIN
LOGIN('Nikita','123456', userid, addressid, customerid, roleuser);
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line(roleuser || ' ' || userid || ' ' || addressid || ' ' || customerid);
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
DROP PROCEDURE PERSONAINFOUPDATE;
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
-------------------------------------------------EXAMPLE PersonalInfoUpdate-----------------------------------------------------
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
DROP PROCEDURE SHOWCATALOG;
CREATE OR REPLACE PROCEDURE SHOWCATALOG(p_productid OUT "PRODUCT"."ProductID"%TYPE, p_name OUT "PRODUCT"."Name"%TYPE,
                                        p_manufacturer OUT "PRODUCT"."Manufacturer"%TYPE, p_price OUT "PRODUCT"."Price"%TYPE) IS
BEGIN
FOR rec IN (
SELECT PRODUCT."ProductID", PRODUCT."Name", PRODUCT."Manufacturer", PRODUCT."Price"
INTO p_productid, p_name, p_manufacturer, p_price
FROM PRODUCT)
LOOP
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line( rec."ProductID" || ' ' || rec."Name" || ' ' || rec."Manufacturer" || ' ' || rec."Price");
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
SHOWCATALOG(productid, name, manufacturer, price);
END;
--------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------BasketDeleteOne---------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
DROP PROCEDURE BASKETDELETEONE;
CREATE OR REPLACE PROCEDURE BASKETDELETEONE(p_basketid IN BASKET."BasketID"%TYPE,
                                            p_customerid IN BASKET."CustomerID"%TYPE) IS
BEGIN
DELETE FROM BASKET WHERE BASKET."BasketID" = p_basketid AND BASKET."CustomerID" = p_customerid;
UPDATE ORDERHISTORY SET ORDERHISTORY."Status" = 'Delete from basket' WHERE ORDERHISTORY."CustomerID" = p_customerid AND 
       ORDERHISTORY."KeyFindProduct" = p_basketid;
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
------------------------------------------------------SHOWREVIEW----------------------------------------------------------------

CREATE OR REPLACE PROCEDURE SHOWREVIEW(p_productid IN PRODUCT."ProductID"%TYPE) AS
BEGIN
FOR rec IN (
SELECT PRODUCT."Name", CUSTOMER."FirstName", REVIEW."Description", REVIEW."Rating", REVIEW."Date"
FROM REVIEW INNER JOIN PRODUCT ON REVIEW."ProductID" = PRODUCT."ProductID" 
            INNER JOIN CUSTOMER ON REVIEW."CustomerID" = CUSTOMER."CustomerID" 
            WHERE REVIEW."ProductID" = p_productid)
LOOP
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line( rec."Name" || ' ' || rec."FirstName" || ' ' || rec."Description" || ' ' || 
                      rec."Rating" || ' ' || rec."Date");
END LOOP;
END SHOWREVIEW;
-------------------------------------------------EXAMPLE SHOWREVIEW-------------------------------------------------------------
BEGIN
SHOWREVIEW(21);
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------SHOWBASKET----------------------------------------------------------------

CREATE OR REPLACE PROCEDURE SHOWBASKET(p_customerid IN CUSTOMER."CustomerID"%TYPE) AS
BEGIN
FOR rec IN (
SELECT PRODUCT."Name", CUSTOMER."FirstName", BASKET."Amount", BASKET."Price"
FROM BASKET INNER JOIN PRODUCT ON BASKET."ProductID" = PRODUCT."ProductID" 
            INNER JOIN CUSTOMER ON BASKET."CustomerID" = CUSTOMER."CustomerID" 
            WHERE BASKET."CustomerID" = p_customerid)
LOOP
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line( rec."Name" || ' ' || rec."FirstName" || ' ' || rec."Amount" || ' ' || 
                      rec."Price");
END LOOP;
END SHOWBASKET;
-------------------------------------------------EXAMPLE SHOWBASKET-------------------------------------------------------------
BEGIN
SHOWBASKET(41);
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------SHOWFAVORITE--------------------------------------------------------------

CREATE OR REPLACE PROCEDURE SHOWFAVORITE(p_customerid IN CUSTOMER."CustomerID"%TYPE) AS
BEGIN
FOR rec IN (
SELECT PRODUCT."Name", CUSTOMER."FirstName"
FROM FAVORITE INNER JOIN PRODUCT ON FAVORITE."ProductID" = PRODUCT."ProductID" 
            INNER JOIN CUSTOMER ON FAVORITE."CustomerID" = CUSTOMER."CustomerID" 
            WHERE FAVORITE."CustomerID" = p_customerid)
LOOP
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line( rec."Name" || ' ' || rec."FirstName");
END LOOP;
END SHOWFAVORITE;
-------------------------------------------------EXAMPLE SHOWFAVORITE-----------------------------------------------------------
BEGIN
SHOWFAVORITE(41);
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------SHOWFAVORITE--------------------------------------------------------------

CREATE OR REPLACE PROCEDURE SHOWHISTORY(p_customerid IN CUSTOMER."CustomerID"%TYPE) AS
BEGIN
FOR rec IN (
SELECT PRODUCT."Name", CUSTOMER."FirstName", ORDERHISTORY."Status", ORDERHISTORY."Date", ORDERHISTORY."Amount", ORDERHISTORY."Price"
FROM ORDERHISTORY INNER JOIN PRODUCT ON ORDERHISTORY."ProductID" = PRODUCT."ProductID" 
            INNER JOIN CUSTOMER ON ORDERHISTORY."CustomerID" = CUSTOMER."CustomerID" 
            WHERE ORDERHISTORY."CustomerID" = p_customerid)
LOOP
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line( rec."Name" || ' ' || rec."FirstName" || ' ' || rec."Status" || ' ' || rec."Date" || ' ' || rec."Amount" || ' ' || rec."Price");
END LOOP;
END SHOWHISTORY;
-------------------------------------------------EXAMPLE SHOWFAVORITE-----------------------------------------------------------
BEGIN
SHOWHISTORY(41);
END;
--------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------SHOWOPTIONPRODUCT--------------------------------------------------------------

CREATE OR REPLACE PROCEDURE SHOWOPTIONPRODUCT(p_productid IN PRODUCT."ProductID"%TYPE) AS
BEGIN
FOR rec IN (
SELECT APPUSER."PRODUCT"."Name", APPUSER."OPTIONTYPE"."OptionTypeName", APPUSER."OPTION"."OptionName", 
       APPUSER."PRODUCTOPTION"."Value", APPUSER."PRODUCTOPTION"."Unit"
FROM APPUSER."PRODUCTOPTION" INNER JOIN APPUSER."PRODUCT" ON APPUSER."PRODUCTOPTION"."ProductID" = APPUSER."PRODUCT"."ProductID" 
            INNER JOIN APPUSER."OPTION" ON APPUSER."PRODUCTOPTION"."OptionID" = APPUSER."OPTION"."OptionID" 
            INNER JOIN APPUSER."OPTIONTYPE" ON APPUSER."OPTION"."OptionTypeID" = APPUSER."OPTIONTYPE"."OptionTypeID" 
            WHERE APPUSER."PRODUCTOPTION"."ProductID" = p_productid)
LOOP
DBMS_OUTPUT.enable;
DBMS_OUTPUT.put_line( rec."Name" || ' ' || rec."OptionTypeName" || ' ' || rec."OptionName" || ' ' || rec."Value" || ' ' || 
                      rec."Unit");
END LOOP;
END SHOWOPTIONPRODUCT;
-------------------------------------------------EXAMPLE SHOWOPTIONPRODUCT-----------------------------------------------------------
BEGIN
SHOWOPTIONPRODUCT(21);
END;
---------------------------------------------------------EXPORT-----------------------------------------------------------------
--1 способ
SET HEADING OFF;
SET FEEDBACK OFF;
spool D:\CP\test.xml;
SET LINESIZE 10000;
SELECT XMLElement("User",
                XMLAttributes("UserID" as "userid"), 
                XMLAgg(
                    XMLElement("row", XMLForest(USERS."Username" "Username", USERS."Password" "Password")
                    )
                    ))
                    as XMLRusult FROM USERS GROUP BY USERS."UserID";
spoo off;
--2 способ

CREATE OR REPLACE DIRECTORY XML_DIR as 'D:\CP\XML';
CREATE OR REPLACE PROCEDURE TABLE_TO_XML_FILE(table_name IN VARCHAR2) 
AS
ctx DBMS_XMLGEN.CTXHANDLE; 
clb CLOB; 
file UTL_FILE.FILE_TYPE; 
buffer VARCHAR2(32767); 
position PLS_INTEGER := 1; 
chars PLS_INTEGER := 32767; 
BEGIN 
ctx := DBMS_XMLGEN.NEWCONTEXT('SELECT * FROM "' || table_name || '"'); 
DBMS_XMLGEN.SETROWSETTAG(ctx, 'RECORDS'); 
DBMS_XMLGEN.SETROWTAG(ctx, 'RECORD'); 
SELECT XMLSERIALIZE(DOCUMENT XMLELEMENT("XML", XMLELEMENT(EVALNAME(table_name), DBMS_XMLGEN.GETXMLTYPE(ctx))) INDENT SIZE = 2) 
INTO clb FROM dual; 
DBMS_XMLGEN.CLOSECONTEXT(ctx); 
file := UTL_FILE.FOPEN('XML_DIR', table_name || '.xml', 'w', 32767); 
WHILE position < DBMS_LOB.GETLENGTH(clb) 
LOOP DBMS_LOB.READ(clb, chars, position, buffer); 
UTL_FILE.PUT(file, buffer); 
UTL_FILE.FFLUSH(file); 
position := position + chars; 
END LOOP; 
UTL_FILE.FCLOSE(file); 
COMMIT; 
END TABLE_TO_XML_FILE;

-----------------------------------------------------IMPORT---------------------------------------------------------------------
DECLARE 
xml_file BFILE; 
xml_data CLOB; 
BEGIN 
    xml_file := BFILENAME ('XML_DIR', 'USERS.xml'); 
    DBMS_LOB.createtemporary (xml_data, TRUE, DBMS_LOB.SESSION); 
    DBMS_LOB.fileopen (xml_file, DBMS_LOB.file_readonly); 
    DBMS_LOB.loadfromfile (xml_data, xml_file, DBMS_LOB.getlength(xml_file)); 
    DBMS_LOB.fileclose (xml_file); 
    INSERT INTO USERS (USERS."Username",USERS."Password",USERS."Role") SELECT ExtractValue(Value(x),'//Username') as login, 
                       ExtractValue(Value(x),'//Password') as password, ExtractValue(Value(x),'//Role') as user_type 
                       FROM TABLE(XMLSequence(Extract(XMLType(xml_data),'/XML/USERS/RECORDS/RECORD'))) x; 
    DBMS_OUTPUT.PUT_LINE( SQL%ROWCOUNT || ' rows inserted.' ); 
    DBMS_LOB.freetemporary (xml_data);                   
    COMMIT; 
END;
EXPLAIN PLAN FOR SELECT * FROM PRODUCT WHERE PRODUCT."ProductID" > 200000;
SELECT * FROM TABLE(dbms_xplan.display(null,null,'basic'));

DECLARE 
i NUMBER := 1;
BEGIN
LOOP
INSERT INTO PRODUCT(PRODUCT."Name", PRODUCT."Manufacturer", PRODUCT."Price") VALUES ('name', 'brand', 1000);
COMMIT;
i := i + 1;
EXIT WHEN (i > 100000);
END LOOP;
END;


