-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: grocery_inventory
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `category_id` int NOT NULL,
  `category_name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Fruits'),(2,'Vegetables'),(3,'Dairy'),(4,'Beverages'),(5,'Snacks'),(6,'Meat'),(7,'Seafood'),(8,'Bakery'),(9,'Canned Goods'),(10,'Frozen Foods');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `grocery_transactions`
--

DROP TABLE IF EXISTS `grocery_transactions`;
/*!50001 DROP VIEW IF EXISTS `grocery_transactions`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `grocery_transactions` AS SELECT 
 1 AS `sales_id`,
 1 AS `product_id`,
 1 AS `product_name`,
 1 AS `sales_date`,
 1 AS `unit_price`,
 1 AS `quantity`,
 1 AS `total_price`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `inventory_stock`
--

DROP TABLE IF EXISTS `inventory_stock`;
/*!50001 DROP VIEW IF EXISTS `inventory_stock`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `inventory_stock` AS SELECT 
 1 AS `product_id`,
 1 AS `product_name`,
 1 AS `category_name`,
 1 AS `unit_price`,
 1 AS `quantity_stock`,
 1 AS `expiration_date`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `product_id` int NOT NULL,
  `product_name` varchar(255) DEFAULT NULL,
  `category_id` int DEFAULT NULL,
  `unit_price` decimal(10,2) DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `expiration_date` date DEFAULT NULL,
  PRIMARY KEY (`product_id`),
  KEY `category_id` (`category_id`),
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `category` (`category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'Apple',1,1.99,100,'2024-02-28'),(2,'Beef',6,5.99,30,'2024-02-20'),(3,'Cabbage',2,0.75,200,'2024-03-05'),(4,'Frozen Nuggets',10,4.50,40,'2024-03-10'),(5,'Milk',3,2.49,55,'2024-02-22'),(6,'Orange Juice',4,3.99,80,'2024-02-15'),(7,'Potato Chips',5,2.75,100,'2024-02-18'),(8,'Salmon',7,8.99,20,'2024-02-24'),(9,'Sardines',9,1.25,60,'2024-02-28'),(10,'Sliced Bread',8,1.49,60,'2024-03-01');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase`
--

DROP TABLE IF EXISTS `purchase`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `purchase` (
  `purchase_id` int NOT NULL,
  `product_id` int DEFAULT NULL,
  `supplier_id` int DEFAULT NULL,
  `purchase_date` date DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `total_price` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`purchase_id`),
  KEY `product_id` (`product_id`),
  KEY `supplier_id` (`supplier_id`),
  CONSTRAINT `purchase_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `product` (`product_id`),
  CONSTRAINT `purchase_ibfk_2` FOREIGN KEY (`supplier_id`) REFERENCES `supplier` (`supplier_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase`
--

LOCK TABLES `purchase` WRITE;
/*!40000 ALTER TABLE `purchase` DISABLE KEYS */;
INSERT INTO `purchase` VALUES (1,1,1,'2024-01-01',50,75.00),(2,2,6,'2024-01-02',75,56.25),(3,3,2,'2024-01-03',25,62.50),(4,4,4,'2024-01-04',30,60.00),(5,5,3,'2024-01-05',15,75.00),(6,6,4,'2024-01-06',100,200.00),(7,7,5,'2024-01-07',150,150.00),(8,8,7,'2024-01-08',60,180.00),(9,9,9,'2024-01-09',40,180.00);
/*!40000 ALTER TABLE `purchase` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `purchase_history`
--

DROP TABLE IF EXISTS `purchase_history`;
/*!50001 DROP VIEW IF EXISTS `purchase_history`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `purchase_history` AS SELECT 
 1 AS `purchase_id`,
 1 AS `product_id`,
 1 AS `product_name`,
 1 AS `supplier_name`,
 1 AS `purchase_date`,
 1 AS `quantity`,
 1 AS `total_price`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `sales`
--

DROP TABLE IF EXISTS `sales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sales` (
  `sales_id` int NOT NULL,
  `product_id` int DEFAULT NULL,
  `sales_date` date DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `total_sales` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`sales_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `sales_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `product` (`product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales`
--

LOCK TABLES `sales` WRITE;
/*!40000 ALTER TABLE `sales` DISABLE KEYS */;
INSERT INTO `sales` VALUES (1,1,'2024-01-01',30,45.00),(2,2,'2024-01-02',60,45.00),(3,3,'2024-01-03',20,50.00),(4,4,'2024-01-04',20,40.00),(5,5,'2024-01-05',5,25.00),(6,6,'2024-01-06',50,500.00),(7,7,'2024-01-07',100,100.00),(8,8,'2024-01-08',30,90.00),(9,9,'2024-01-09',20,90.00),(10,10,'2024-01-10',40,50.00),(11,9,'2024-01-11',10,12.50);
/*!40000 ALTER TABLE `sales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplier`
--

DROP TABLE IF EXISTS `supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplier` (
  `supplier_id` int NOT NULL,
  `supplier_name` varchar(255) DEFAULT NULL,
  `contact_person` varchar(255) DEFAULT NULL,
  `contact_number` varchar(20) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`supplier_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier`
--

LOCK TABLES `supplier` WRITE;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
INSERT INTO `supplier` VALUES (1,'Fresh Produce Inc.','John Smith','09234567890','johnsmith@email.com'),(2,'Dairy Farm Co.','Emma Johnson','09987654321','emmajohnson@email.com'),(3,'Beverage Distributors Ltd.','Michael Davis','09122334455','michaeldavis@email.com'),(4,'Snack Suppliers LLC','Emily Wilson','09567890123','emilywilson@email.com'),(5,'Green Grocers','David Brown','09445556668','davidbrown@email.com'),(6,'Meat Masters','Mark Johnson','09654321876','markjohnson@email.com'),(7,'Seafood Suppliers Inc.','Sarah Lee','09876543210','sarahlee@email.com'),(8,'Bakery Heaven','Jessica Miller','09765432109','jessicamiller@email.com'),(9,'Canned Goods Co.','Kevin White','09187654321','kevinwhite@email.com');
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `emp_ID` int NOT NULL AUTO_INCREMENT,
  `emp_email` varchar(255) DEFAULT NULL,
  `emp_pass` varchar(255) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `first_name` varchar(255) DEFAULT NULL,
  `surname` varchar(255) DEFAULT NULL,
  `phone` varchar(255) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`emp_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'allana.nual@sim.com','allana25','allana.nual25','Allana','Nual','09218448664','Active'),(2,'vivian.vivo@sim.com','vianvivo','vivian_vivo','Vivian','Vivo','09234567890','Inactive'),(3,'kent.delapena@sim.com','arjaykent','kent_arjay','Kent','Dela Pena','09123456789','Inactive'),(4,'kim.dionisio@sim.com','kimd1234','kim_dio','Kim','Dionisio','09213213945','Inactive'),(5,'eijla.deguzman@sim.com','eylaaa18','eyla_dee','Eijla','de Guzman','09876543210','Inactive'),(6,'nherlhin.guevarra@sim.com','nherlhin13','nerlin_gue','Nherlhin','Guevarra','09098765432','Inactive'),(7,'paul.moit@sim.com','pollmoyt','paul_moit','Paul','Moit','09218387328','Inactive'),(9,'ivan.mendoza@sim.com','aybanmen','ivan_mendoza','Ivan','Mendoza','09201832174','Inactive'),(10,'diogenes.tayam@sim.com','diogenes7','diogenes_tayam','Diogenes','Tayam','09217456329','Inactive');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Final view structure for view `grocery_transactions`
--

/*!50001 DROP VIEW IF EXISTS `grocery_transactions`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `grocery_transactions` AS select `s`.`sales_id` AS `sales_id`,`s`.`product_id` AS `product_id`,`p`.`product_name` AS `product_name`,`s`.`sales_date` AS `sales_date`,`p`.`unit_price` AS `unit_price`,`s`.`quantity` AS `quantity`,(`s`.`quantity` * `p`.`unit_price`) AS `total_price` from (`sales` `s` join `product` `p` on((`s`.`product_id` = `p`.`product_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `inventory_stock`
--

/*!50001 DROP VIEW IF EXISTS `inventory_stock`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `inventory_stock` AS select `p`.`product_id` AS `product_id`,`p`.`product_name` AS `product_name`,`c`.`category_name` AS `category_name`,`p`.`unit_price` AS `unit_price`,`p`.`quantity` AS `quantity_stock`,`p`.`expiration_date` AS `expiration_date` from (`product` `p` join `category` `c` on((`p`.`category_id` = `c`.`category_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `purchase_history`
--

/*!50001 DROP VIEW IF EXISTS `purchase_history`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `purchase_history` AS select `pr`.`purchase_id` AS `purchase_id`,`pr`.`product_id` AS `product_id`,`p`.`product_name` AS `product_name`,`s`.`supplier_name` AS `supplier_name`,`pr`.`purchase_date` AS `purchase_date`,`pr`.`quantity` AS `quantity`,`pr`.`total_price` AS `total_price` from ((`purchase` `pr` join `product` `p` on((`pr`.`product_id` = `p`.`product_id`))) join `supplier` `s` on((`pr`.`supplier_id` = `s`.`supplier_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-28 23:16:44
