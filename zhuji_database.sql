/*
 Navicat Premium Data Transfer

 Source Server         : MySQL@Local
 Source Server Type    : MySQL
 Source Server Version : 50134
 Source Host           : localhost:3306
 Source Schema         : zhuji_database

 Target Server Type    : MySQL
 Target Server Version : 50134
 File Encoding         : 65001

 Date: 03/09/2019 10:53:19
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for tbiz_movie
-- ----------------------------
DROP TABLE IF EXISTS `tbiz_movie`;
CREATE TABLE `tbiz_movie`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MovieFileName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '电影文件名',
  `MovieName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL,
  `CategoryByLocal` int(255) NULL DEFAULT NULL COMMENT '外加之地区分类:美国,国产,香港,台湾,韩国,日本,...',
  `SaveLocal` tinyint(255) NULL DEFAULT NULL,
  `PublicDate` year(4) NULL DEFAULT NULL,
  `CreateTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

-- ----------------------------
-- Records of tbiz_movie
-- ----------------------------
INSERT INTO `tbiz_movie` VALUES (1, '阿甘正传', '阿甘正传', NULL, NULL, NULL, '2019-08-13 10:35:06');
INSERT INTO `tbiz_movie` VALUES (2, '肖申克的救赎', '肖申克的救赎', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (3, '烈火英雄', '烈火英雄', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (4, '哪吒之魔童降世', '哪吒之魔童降世', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (5, '复仇者联盟4X', '复仇者联盟4X', 1, 0, 2019, NULL);
INSERT INTO `tbiz_movie` VALUES (6, '好莱坞往事', '好莱坞往事', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (7, '疾速备战3', '疾速备战3', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (8, '千与千寻', '千与千寻', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (9, '何以为家', '何以为家', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (10, '我不是药神', '我不是药神', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (11, '寻梦环游记', '寻梦环游记', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (12, '阿丽塔:战斗天使', '阿丽塔:战斗天使', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (13, '绝杀慕尼黑', '绝杀慕尼黑', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (14, '疾速特攻', '疾速特攻', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (15, '流浪地球', '流浪地球', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (16, '蜘蛛侠:英雄远征', '蜘蛛侠:英雄远征', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (17, '妈阁是座城', '妈阁是座城', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (18, '西虹市首富', '西虹市首富', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (19, '追龙', '追龙', NULL, NULL, NULL, NULL);
INSERT INTO `tbiz_movie` VALUES (20, '无问西东', '无问西东', 0, 1, 2017, NULL);
INSERT INTO `tbiz_movie` VALUES (21, '战狼2', NULL, 3, 1, 2009, '2019-08-01 10:36:06');
INSERT INTO `tbiz_movie` VALUES (22, '指环王', '指环王', 0, 0, 2019, '2019-08-21 11:03:48');
INSERT INTO `tbiz_movie` VALUES (23, '骇客帝国', '骇客帝国', 0, 0, 2009, '2019-08-21 11:08:31');
INSERT INTO `tbiz_movie` VALUES (24, '中华英雄', '中华英雄', 0, 0, 2010, '2019-08-21 11:11:27');
INSERT INTO `tbiz_movie` VALUES (25, '中华英雄', NULL, 0, 0, NULL, '2019-08-21 11:12:21');
INSERT INTO `tbiz_movie` VALUES (26, '蜘蛛侠', '蜘蛛侠', 0, 0, NULL, '2019-08-21 13:27:00');
INSERT INTO `tbiz_movie` VALUES (27, '大东海', NULL, 0, 0, NULL, '2019-08-21 13:32:58');
INSERT INTO `tbiz_movie` VALUES (28, '大东海', NULL, 0, 0, NULL, '2019-08-21 14:10:24');
INSERT INTO `tbiz_movie` VALUES (29, '黄金帝国', NULL, 0, 0, NULL, '2019-08-21 14:17:03');
INSERT INTO `tbiz_movie` VALUES (30, '大东海', NULL, 0, 0, NULL, '2019-08-21 14:21:14');
INSERT INTO `tbiz_movie` VALUES (31, '大西豪', NULL, 0, 0, NULL, '2019-08-21 14:30:12');
INSERT INTO `tbiz_movie` VALUES (32, '日本国', NULL, 0, 0, NULL, '2019-08-21 14:43:16');
INSERT INTO `tbiz_movie` VALUES (33, '洗澡3', NULL, 1, 1, 2010, '2019-08-21 15:02:50');
INSERT INTO `tbiz_movie` VALUES (34, '洗澡2', NULL, 0, 0, NULL, '2019-08-21 15:03:29');

-- ----------------------------
-- Table structure for tbiz_picture
-- ----------------------------
DROP TABLE IF EXISTS `tbiz_picture`;
CREATE TABLE `tbiz_picture`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `FileName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '文件名',
  `FileDirectory` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL,
  `ReferenceID` int(255) NULL DEFAULT NULL,
  `DataType` int(255) NULL DEFAULT NULL COMMENT '图片类别,电影,文章,音乐,等等',
  `Property` int(255) NULL DEFAULT NULL,
  `CreateTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

-- ----------------------------
-- Records of tbiz_picture
-- ----------------------------
INSERT INTO `tbiz_picture` VALUES (1, '01990a52-b6d1-4096-a160-410e7f77bcce.jpg', '\\ImageBase\\Movie\\', 21, 1, NULL, '2019-08-16 15:48:32');
INSERT INTO `tbiz_picture` VALUES (2, '3d436c3f-9752-4aed-9994-a273edf3266d.jpg', '\\ImageBase\\Movie\\', 20, 1, NULL, '2019-08-20 15:04:54');
INSERT INTO `tbiz_picture` VALUES (3, '2ae4a943-d643-4f0b-9031-4d1c0f6925d0.jpg', '\\ImageBase\\Movie\\', 0, 1, NULL, '2019-08-21 11:03:48');
INSERT INTO `tbiz_picture` VALUES (4, '9eba362c-f6ed-476c-8721-67e07c336af9.jpg', '\\ImageBase\\Movie\\', 0, 1, NULL, '2019-08-21 11:11:27');
INSERT INTO `tbiz_picture` VALUES (5, 'd2334ca9-f79b-4622-8eb1-f28550ace2ee.jpg', '\\ImageBase\\Movie\\', 0, 1, NULL, '2019-08-21 11:13:21');
INSERT INTO `tbiz_picture` VALUES (6, 'a61cba59-9d5c-404b-adda-7c95974b2140.jpg', '\\ImageBase\\Movie\\', 0, 1, NULL, '2019-08-21 13:27:07');
INSERT INTO `tbiz_picture` VALUES (7, '05b83d52-60f3-4444-b39b-f12a9b862260.jpg', '\\ImageBase\\Movie\\', 0, 1, NULL, '2019-08-21 14:10:39');
INSERT INTO `tbiz_picture` VALUES (8, '4ac84229-3755-4d86-975f-05e267e621b3.jpg', '\\ImageBase\\Movie\\', 30, 1, NULL, '2019-08-21 14:21:23');
INSERT INTO `tbiz_picture` VALUES (9, '02e9e56d-fd44-41e4-ae09-7cfcb6b7404c.jpg', '\\ImageBase\\Movie\\', 31, 1, NULL, '2019-08-21 14:30:33');
INSERT INTO `tbiz_picture` VALUES (10, 'f11cea87-e6d8-4a8d-97aa-a65f431d9cbd.jpg', '\\ImageBase\\Movie\\', 32, 1, NULL, '2019-08-21 14:43:24');
INSERT INTO `tbiz_picture` VALUES (11, '1fe26152-3273-481b-b449-6696d021ffe8.jpg', '\\ImageBase\\Movie\\', 34, 1, NULL, '2019-08-21 16:04:06');

-- ----------------------------
-- Table structure for tbiz_question
-- ----------------------------
DROP TABLE IF EXISTS `tbiz_question`;
CREATE TABLE `tbiz_question`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `QuestionTitle` varchar(500) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '题目题面',
  `SubjectType` int(255) NULL DEFAULT NULL COMMENT '科目分类',
  `QuestionType` tinyint(255) NULL DEFAULT NULL COMMENT '题型属性',
  `QuestionOptionIds` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '题目选项ID',
  `AnswerIds` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '答案选项ID',
  `CreateTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for tbiz_questionanswer
-- ----------------------------
DROP TABLE IF EXISTS `tbiz_questionanswer`;
CREATE TABLE `tbiz_questionanswer`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `AnswerTitle` varchar(500) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '答案字面',
  `QuestionId` int(255) NULL DEFAULT NULL COMMENT '外键题目Id',
  `OptionIds` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '选择题答案选项ID',
  `PublicDate` year(4) NULL DEFAULT NULL,
  `CreateTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for tbiz_questionoption
-- ----------------------------
DROP TABLE IF EXISTS `tbiz_questionoption`;
CREATE TABLE `tbiz_questionoption`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `OptionTitle` varchar(500) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '选项字面',
  `IsAnswer` int(255) NULL DEFAULT NULL COMMENT '是否是答案',
  `QuestionId` int(255) NULL DEFAULT NULL COMMENT '外键题目Id',
  `OptionType` int(255) NULL DEFAULT NULL COMMENT '选项类型',
  `PublicDate` year(4) NULL DEFAULT NULL,
  `CreateTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for tcfg_dictitem
-- ----------------------------
DROP TABLE IF EXISTS `tcfg_dictitem`;
CREATE TABLE `tcfg_dictitem`  (
  `DictId` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `ParentID` int(255) NULL DEFAULT NULL,
  `TableName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '表名',
  `FieldName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL,
  `PropertyName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '属性名',
  `PropertyValue` int(255) NULL DEFAULT NULL,
  `OrderNumber` int(255) NULL DEFAULT NULL,
  `IsDelete` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`DictId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 17 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

-- ----------------------------
-- Records of tcfg_dictitem
-- ----------------------------
INSERT INTO `tcfg_dictitem` VALUES (1, NULL, 'movie', 'categoryByLocal', '美国', 1, 1, NULL);
INSERT INTO `tcfg_dictitem` VALUES (2, NULL, 'movie', 'categoryByLocal', '韩国', 2, 2, NULL);
INSERT INTO `tcfg_dictitem` VALUES (3, NULL, 'movie', 'categoryByLocal', '中国', 3, 3, NULL);
INSERT INTO `tcfg_dictitem` VALUES (4, NULL, 'movie', 'categoryByLocal', '中国香港', 4, 4, NULL);
INSERT INTO `tcfg_dictitem` VALUES (5, NULL, 'movie', 'categoryByLocal', '其它', 0, 0, NULL);
INSERT INTO `tcfg_dictitem` VALUES (6, NULL, 'movie', 'saveLocal', '其它', 0, 0, NULL);
INSERT INTO `tcfg_dictitem` VALUES (7, NULL, 'movie', 'saveLocal', '2T_1', 1, 1, NULL);
INSERT INTO `tcfg_dictitem` VALUES (8, NULL, 'movie', 'saveLocal', '2T_2', 2, 2, NULL);
INSERT INTO `tcfg_dictitem` VALUES (9, NULL, 'movie', 'saveLocal', '2T_3', 3, 3, NULL);
INSERT INTO `tcfg_dictitem` VALUES (10, NULL, 'movie', 'saveLocal', '2T_4', 4, 4, NULL);
INSERT INTO `tcfg_dictitem` VALUES (11, NULL, 'question', 'subjectType', '语文', 1, 1, NULL);
INSERT INTO `tcfg_dictitem` VALUES (12, NULL, 'question', 'subjectType', '数学', 2, 2, NULL);
INSERT INTO `tcfg_dictitem` VALUES (13, NULL, 'question', 'subjectType', '英文', 3, 3, NULL);
INSERT INTO `tcfg_dictitem` VALUES (14, NULL, 'question', 'questionType', '非选题', 1, NULL, NULL);
INSERT INTO `tcfg_dictitem` VALUES (15, NULL, 'question', 'questionType', '单选题', 2, NULL, NULL);
INSERT INTO `tcfg_dictitem` VALUES (16, NULL, 'question', 'questionType', '多选题', 3, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
