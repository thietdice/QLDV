-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: qldv
-- ------------------------------------------------------
-- Server version	8.0.33

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
-- Table structure for table `articles`
--

DROP TABLE IF EXISTS `articles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `articles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `viewed` int DEFAULT NULL,
  `title` varchar(255) NOT NULL,
  `content` varchar(255) DEFAULT NULL,
  `pdf` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `userid_created` int DEFAULT NULL,
  `userid_updated` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `articles`
--

LOCK TABLES `articles` WRITE;
/*!40000 ALTER TABLE `articles` DISABLE KEYS */;
INSERT INTO `articles` VALUES (3,1,'VÙNG CÔNG NGHỆ 2022','Campus tour Technology Zone - Together we make IT','326ad0bd-ba23-4571-bef9-a070b0949ad4.pdf','2023-06-06 23:17:58','2023-06-11 04:08:39',3,3),(4,1,'GALA TỔNG KẾT VÀ TRAO GIẢI CÁC CUỘC THI TRONG CHUỖI SỰ KIỆN THE GRATEFULLNESS 2022','GALA TỔNG KẾT VÀ TRAO GIẢI CÁC CUỘC THI TRONG CHUỖI SỰ KIỆN THE GRATEFULLNESS 2022',NULL,'2023-06-06 23:18:59',NULL,3,NULL),(5,0,'Phó bí thư với thành tích tiêu biểu của Liên chi Đoàn Khoa Công nghệ thông tin','Đó là một trong nhiều bí quyết mà sinh viên tiêu biểu Khoa Công nghệ thông tin năm 2022, Hoàng Văn Tuấn (Lớp 19 CN1) đã áp dụng thành công để đem về nhiều thành tích xuất sắc trong học tập và công tác Đoàn, Hội...',NULL,'2023-06-06 23:19:24',NULL,3,NULL),(6,0,'Festival Tết Kiến Trúc 2023 - Hương sắc Tết Việt','Cuộc thi ”Hương sắc Tết Việt” đã kết thúc trong sự thành công mĩ mãn nhất. Cùng ngắm nhìn những khoảnh khắc đầy ý nghĩa của tất cả chúng ta trong Festival Tết lần này nhé.',NULL,'2023-06-06 23:20:37',NULL,3,NULL),(7,0,'Cuộc thi ATC - ALGORITHMIC THINKING CHALLENGE 2022','Tiếp nối chuỗi sự kiện Kỷ niệm 40 năm ngày Nhà giáo Việt Nam của LCĐ khoa Công nghệ thông tin; tạo ra sân chơi lập trình giúp các bạn sinh viên thể hiện năng lực, bản lĩnh, trí tuệ và đam mê với công nghệ',NULL,'2023-06-06 23:21:43',NULL,3,NULL),(8,0,'Cuộc thi MOS - MICROSOFT OFFICE SPECIALIST 2022','Sau những giây phút đấu trí căng thẳng, cuộc thi MOS đã khép lại vô cùng thành công rực rỡ. Cảm ơn các thí sinh đã thi đấu hết mình cho giải đấu ngày hôm nay. Kết quả sẽ sớm được hé lộ sau vài ngày',NULL,'2023-06-06 23:22:25',NULL,3,NULL);
/*!40000 ALTER TABLE `articles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `classes`
--

DROP TABLE IF EXISTS `classes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `classes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `faculty_id` int NOT NULL,
  `title` varchar(255) NOT NULL,
  `description` text,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `userid_created` int DEFAULT NULL,
  `userid_updated` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `classes_ibfk_1` (`faculty_id`),
  CONSTRAINT `classes_ibfk_1` FOREIGN KEY (`faculty_id`) REFERENCES `faculties` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `classes`
--

LOCK TABLES `classes` WRITE;
/*!40000 ALTER TABLE `classes` DISABLE KEYS */;
INSERT INTO `classes` VALUES (8,6,'NT1','32','2023-05-22 04:33:47','2023-05-22 06:48:20',3,3),(9,6,'NT2','321','2023-05-22 04:33:53','2023-05-22 06:48:16',3,3),(10,6,'NT3','213','2023-05-22 04:33:59',NULL,3,NULL),(11,4,'CN4','213','2023-05-22 04:34:16',NULL,3,NULL),(12,4,'CN5','321','2023-05-22 04:34:22',NULL,3,NULL),(13,4,'CN6','321','2023-05-22 04:34:27',NULL,3,NULL);
/*!40000 ALTER TABLE `classes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `event_user`
--

DROP TABLE IF EXISTS `event_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `event_user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `event_id` int NOT NULL,
  `user_id` int NOT NULL,
  `image` text,
  `note` varchar(255) DEFAULT NULL,
  `note_reviewer` varchar(255) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `event_user_ibfk_1` (`user_id`),
  KEY `event_user_ibfk_2` (`event_id`),
  CONSTRAINT `event_user_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  CONSTRAINT `event_user_ibfk_2` FOREIGN KEY (`event_id`) REFERENCES `events` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event_user`
--

LOCK TABLES `event_user` WRITE;
/*!40000 ALTER TABLE `event_user` DISABLE KEYS */;
/*!40000 ALTER TABLE `event_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `events`
--

DROP TABLE IF EXISTS `events`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `events` (
  `id` int NOT NULL AUTO_INCREMENT,
  `semester_id` int NOT NULL,
  `title` varchar(255) NOT NULL,
  `description` text,
  `content` text,
  `day_start` date NOT NULL,
  `day_end` date NOT NULL,
  `score` int NOT NULL,
  `image` text,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `publish` tinyint(1) DEFAULT NULL,
  `userid_created` int DEFAULT NULL,
  `userid_updated` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `semester_id` (`semester_id`),
  CONSTRAINT `events_ibfk_1` FOREIGN KEY (`semester_id`) REFERENCES `semesters` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `events`
--

LOCK TABLES `events` WRITE;
/*!40000 ALTER TABLE `events` DISABLE KEYS */;
INSERT INTO `events` VALUES (13,14,'HỘI NGHỊ KHOA HỌC QUỐC TẾ CIGOS 2024 LẦN THỨ 7','','   Với mục đích trao đổi chuyên môn, thảo luận về những nghiên cứu và phát triển mới nhất trong các lĩnh vực: Quy hoạch, Kiến trúc, Xây dựng, Hạ tầng, Thiết kế Mỹ thuật ứng dụng, Môi trường, Công nghệ kỹ thuật số… tăng cường trao đổi về giảng dạy, nghiên cứu và tư vấn hoạch định chính sách phát triển, Trường Đại học Kiến trúc Thành Phố Hồ Chí Minh (UAH) phối hợp cùng Tổ chức Khoa học và Chuyên gia Việt Nam toàn cầu (AVSE Global) đồng tổ chức Hội nghị Khoa học Quốc tế CIGOS 2024 (lần thứ 7).','2023-06-18','2023-06-21',5,'c59873bd-b85c-431a-b7d2-ee7ae7b67054.jpg','2023-06-01 16:32:09',NULL,1,3,NULL),(14,14,'HỘI THẢO QUỐC TẾ GIỚI THIỆU CHƯƠNG TRÌNH THẠC SĨ, TIẾN SĨ TẠI ĐẠI HỌC CURTIN & CHÍNH SÁCH VIỆC LÀM TẠI ÚC CHO SINH VIÊN SAU TỐT NGHIỆP',NULL,'Đại học Curtin được xếp hạng trong top 1% những Trường Đại học hàng đầu thế giới và đang đào tạo hơn 56,000 sinh viên đến từ khắp nơi trên thế giới. Trường có sự hợp tác với những doanh nghiệp lớn nhất tại Úc cũng như toàn cầu. Những chương trình đào tạo và nghiên cứu của Curtin rất thực tế, có tính ứng dụng và sáng tạo cao, đảm bảo cơ hội nghề nghiệp của sinh viên sau khi tốt nghiệp.','2023-06-19','2023-06-28',10,'fa588278-2588-416c-a91c-3f2bd2902b2c.jpg','2023-06-06 17:35:00','2023-06-06 17:37:03',1,3,3),(15,15,'HỘI THẢO KHOA HỌC QUỐC TẾ “KINH NGHIỆM VIỆT NAM-BA LAN TRONG BẢO TỒN DI SẢN KIẾN TRÚC”',NULL,'Hội thảo được tổ chức để tri ân những đóng góp to lớn của các chuyên gia Ba Lan và Việt Nam trong khôi phục và bảo tồn các công trình di sản đang bị đe dọa tại Việt Nam cũng như những nỗ lực của họ trong việc đào tạo và giáo dục thế hệ chuyên gia bảo tồn tiếp nối. Hội nghị cũng là cơ hội chia sẻ những kiến ​​thức và kinh nghiệm quý báu về bảo tồn và công nghệ thu được trong các dự án nghiên cứu và trùng tu đã hoàn thành ở Ba Lan và Việt Nam. ','0001-01-01','0001-01-01',3,'0c62732a-0f77-4800-807f-0ff19302ac5b.jpg','2023-06-06 17:35:16','2023-06-06 17:37:07',1,3,3);
/*!40000 ALTER TABLE `events` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `faculties`
--

DROP TABLE IF EXISTS `faculties`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `faculties` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  `short_title` varchar(255) NOT NULL,
  `description` text,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `userid_created` int DEFAULT NULL,
  `userid_updated` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `faculties`
--

LOCK TABLES `faculties` WRITE;
/*!40000 ALTER TABLE `faculties` DISABLE KEYS */;
INSERT INTO `faculties` VALUES (4,'Khoa CNTT','CNTT','Liên Chi Đoàn CNTT','2023-05-21 09:11:30','2023-05-28 12:04:37',3,3),(5,'Khoa Kiến Trúc','KT','qew312','2023-05-22 04:33:25',NULL,3,NULL),(6,'Khoa Nội Thất','NT','3213','2023-05-22 04:33:36',NULL,3,NULL);
/*!40000 ALTER TABLE `faculties` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `semesters`
--

DROP TABLE IF EXISTS `semesters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `semesters` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  `day_start` date NOT NULL,
  `day_end` date NOT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `userid_created` int DEFAULT NULL,
  `userid_updated` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `semesters`
--

LOCK TABLES `semesters` WRITE;
/*!40000 ALTER TABLE `semesters` DISABLE KEYS */;
INSERT INTO `semesters` VALUES (14,'Kì 1 Năm Học 2023-2024','2023-06-07','2023-06-21','2023-06-01 14:22:10','2023-06-01 14:28:32',3,3),(15,'Kì 2 Năm Học 2023-2024','2023-07-05','2024-07-05','2023-06-06 16:35:36',NULL,3,NULL);
/*!40000 ALTER TABLE `semesters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_catalogues`
--

DROP TABLE IF EXISTS `user_catalogues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_catalogues` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_catalogues`
--

LOCK TABLES `user_catalogues` WRITE;
/*!40000 ALTER TABLE `user_catalogues` DISABLE KEYS */;
INSERT INTO `user_catalogues` VALUES (1,'Đoàn Viên'),(2,'BCH Chi Đoàn'),(3,'BCH Liên Chi Đoàn'),(4,'BCH Đoàn Trường');
/*!40000 ALTER TABLE `user_catalogues` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_catalogue_id` int NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL,
  `id_student` varchar(100) DEFAULT NULL,
  `class_id` int DEFAULT NULL,
  `fullname` varchar(100) NOT NULL,
  `birthday` date DEFAULT NULL,
  `gender` varchar(100) DEFAULT NULL,
  `ethnic` varchar(100) DEFAULT NULL,
  `religion` varchar(5) DEFAULT NULL,
  `id_card` varchar(100) DEFAULT NULL,
  `profession` varchar(100) DEFAULT NULL,
  `level_education` varchar(100) DEFAULT NULL,
  `level_specialize` varchar(100) DEFAULT NULL,
  `level_politics` varchar(100) DEFAULT NULL,
  `level_computer` varchar(100) DEFAULT NULL,
  `level_language` varchar(100) DEFAULT NULL,
  `day_in_union` date DEFAULT NULL,
  `phone` varchar(50) DEFAULT NULL,
  `image` text,
  `residence_address` varchar(255) DEFAULT NULL,
  `residence_city` varchar(20) DEFAULT NULL,
  `residence_district` varchar(20) DEFAULT NULL,
  `residence_ward` varchar(20) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `userid_created` int DEFAULT NULL,
  `userid_updated` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `users_ibfk_1` (`class_id`),
  KEY `users_ibfk_2_idx` (`user_catalogue_id`),
  CONSTRAINT `users_ibfk_1` FOREIGN KEY (`class_id`) REFERENCES `classes` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `users_ibfk_2` FOREIGN KEY (`user_catalogue_id`) REFERENCES `user_catalogues` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,2,'2','123','1955010201',12,'Nguyễn Văn B',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(3,4,'4','123','1955010202',10,'Nguyễn Văn D',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(4,3,'3','123','1123',12,'Nguyễn Văn C',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'a0b4e8e7-7f86-4e6a-b083-82cc64110972.jpg',NULL,NULL,NULL,NULL,NULL,'2023-06-01 16:38:49',NULL,3),(7,1,'1','123','1123213213',11,'Nguyễn Văn A',NULL,'0','0','0',NULL,'0','Trung học phổ thông hệ 12/12','Tiến sĩ','Trung cấp','Chưa có','0',NULL,NULL,'20a7d041-c1bf-49cd-b1e3-9e2f95ba2e9d.png','123','Thành phố Hà Nội',NULL,NULL,'2023-06-01 16:41:27','2023-06-01 16:41:54',3,7),(11,1,'test1','1','1',12,'test1',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2023-06-06 17:32:46',NULL,3,NULL),(12,1,'test2','1',NULL,9,'test2',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2023-06-06 17:33:14',NULL,3,NULL),(13,1,'test3','1',NULL,9,'test3',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2023-06-06 17:33:27',NULL,3,NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-08-30 16:39:56
