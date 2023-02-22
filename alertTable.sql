# 通用范例
CREATE TABLE `tgfc_tag`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `TagName` varchar(500) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT 'tag字面',
  `Num` int(255) NULL DEFAULT NULL COMMENT '当前tag的引用个数'
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;



#1.文章主表
CREATE TABLE `tbiz_article`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ArticleTitle` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '文章题目',
  `ArticleTitleAlias` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '文章题目别名',
  `ArticleContent` text CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '文章内容',
  `CategoryId` int(255) NULL DEFAULT NULL,
  `IsPublic` smallint  NULL,
  `TagName` varchar(400) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT 'tag标签,以逗号分隔',
  `TagId` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT 'tag标签Id,以逗号分隔',
  `CreateTime` datetime  NULL DEFAULT CURRENT_TIMESTAMP,
  `CreateBy` smallint  NULL,
  `IsDelete` smallint  NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

#2.文章分类表
CREATE TABLE `tbiz_article_category`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PId` int(255) NULL DEFAULT NULL,
  `CategoryName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '分类名',
  `Level` smallint  NULL,
  `HrefTpl` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '静态化Href模板',
  `Order` smallint  NULL,
  `CreateTime` datetime  NULL DEFAULT CURRENT_TIMESTAMP,
  `CreateBy` smallint  NULL,
  `IsDelete` smallint  NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

#2.文章标签表
CREATE TABLE `tgfc_article_tag` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TagName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `Num` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_croatian_ci;

#文章tag关系表
CREATE TABLE `tbiz_article_tag_relationship`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `ArticleId` int(11) NOT NULL,
  `TagId` int(11) NOT NULL,
  `IsDelete` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

mysql5.6.17 支持
SELECT * FROM tcfg_dictitem GROUP BY TableName ORDER BY TableName

#《图书管理》1 图书表  图书卷名
CREATE TABLE `tbiz_book`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `BookName` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '图书名',
  `CategoryId` int(255) NULL DEFAULT NULL,
  `PublicYear` year  NULL DEFAULT NULL,
  `PublisherId` int(255) NULL DEFAULT NULL,
  `OrderNum` smallint  NULL COMMENT '用于排序',
  `TagName` varchar(400) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT 'tag标签,以逗号分隔',
  `TagId` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT 'tag标签Id,以逗号分隔',
  `CreateTime` datetime  NULL DEFAULT CURRENT_TIMESTAMP,
  `CreateBy` smallint  NULL,
  `IsDelete` smallint  NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

#图书管理2
CREATE TABLE `tbiz_book_detail`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `BookId` int(11) NOT NULL,
  `ColumnId` int(11) NULL COMMENT '套装名',
  `BookContent` text CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '文章内容',
  `CreateBy` smallint  NULL,
  `IsDelete` smallint  NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

#图书管理3 字典表
CREATE TABLE `tbiz_book_dictionary` (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PropertyName` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `OrderNum` smallint  NULL COMMENT '用于排序',
  `PId` int(255) NULL DEFAULT NULL COMMENT '字典类别表', 
   PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

 
#图书管理4 创建一个卷表 不是所有的书都有卷, 但有需要知道相关卷有哪些书   书, 卷(column) 关系管理  
CREATE TABLE `tbiz_book_column` (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ColumnName` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '图书卷名',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;



#书相关图片关系  一对多
CREATE TABLE `tbiz_book_image` (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `BookId` int(11) NOT NULL,
  `OrderNum` smallint  NULL COMMENT '用于排序',
  `ImagePath` varchar(200),
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_croatian_ci ROW_FORMAT = Compact;

#视频教程
CREATE TABLE `tbiz_it_video`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MovieFileName` varchar(255) CHARACTER SET utf8 COLLATE utf8_croatian_ci NULL DEFAULT NULL COMMENT '文件名',
  `MovieName` varchar(255) CHARACTER SET utf8 COLLATE utf8_croatian_ci NULL DEFAULT NULL,
  `CategoryByOne` int(255) NULL DEFAULT NULL COMMENT '分类一,大分类',
  `CategoryByTwo` int(255) NULL DEFAULT NULL COMMENT '分类二,出版实体',
  `CategoryByThree` int(255) NULL DEFAULT NULL COMMENT '分类三,讲师',
  `Remarks` text CHARACTER SET utf8 COLLATE utf8_croatian_ci NULL DEFAULT NULL COMMENT '备注',
  `PublicDate` year(4) NULL DEFAULT NULL,
  `CreateTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_croatian_ci ROW_FORMAT = Compact;

#视频教程
CREATE TABLE `tbiz_it_video_dictitem`  (
  `DictId` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `ParentID` int(255) NULL DEFAULT NULL,
  `TableName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '表名',
  `FieldName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL,
  `PropertyName` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '属性名',
  `PropertyValue` int(255) NULL DEFAULT NULL,
  `OrderNumber` int(255) NULL DEFAULT NULL,
  `IsDelete` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`DictId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;


ALTER TABLE tbiz_it_video MODIFY COLUMN MovieFileName VARCHAR(255) BINARY CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL;

# 音乐主表 国语/粤语/英语   纯音乐/流行歌曲   
                            钢琴/竖琴/萨克斯/电子乐   经典翻唱
CREATE TABLE `tbiz_music`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MusicName` varchar(255) CHARACTER SET utf8 COLLATE utf8_croatian_ci NULL DEFAULT NULL COMMENT '专辑名',
  `MusicFileName` varchar(255) CHARACTER SET utf8 COLLATE utf8_croatian_ci NULL DEFAULT NULL COMMENT '文件夹名字',
  `LanguageType` tinyint(50) NULL DEFAULT NULL,
  `CategoryByOne` int(255) NULL DEFAULT NULL COMMENT '分类一,',
  `CategoryByTwo` int(255) NULL DEFAULT NULL COMMENT '分类二,',
  `CategoryByThree` int(255) NULL DEFAULT NULL COMMENT '分类三,',
  `PublicDate` year(4) NULL DEFAULT NULL,
  `ArtistId` int(11) NOT NULL,
  `Remarks` text CHARACTER SET utf8 COLLATE utf8_croatian_ci NULL DEFAULT NULL COMMENT '备注',  
  `CreateTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_croatian_ci ROW_FORMAT = Compact;

CREATE TABLE `tbiz_music_artist`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ArtistName` varchar(50) CHARACTER SET utf8 COLLATE utf8_croatian_ci NULL DEFAULT NULL COMMENT '艺术家名',
  `CategoryByLocal` int(255) NULL DEFAULT NULL COMMENT '外加之地区分类:美国,国产,香港,台湾,韩国,日本,...',
  `Gender` tinyint(50) NULL DEFAULT NULL,
  `CreateTime` datetime  NULL DEFAULT CURRENT_TIMESTAMP,
  `CreateBy` smallint  NULL,
  `IsDelete` smallint  NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_croatian_ci ROW_FORMAT = Compact;


# 业务表-用户信息收集表
CREATE TABLE `tbiz_person` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PersonName` varchar(30) COLLATE utf8_croatian_ci NOT NULL COMMENT '用户姓名',
  `PersonType` smallint(6) DEFAULT 1 COMMENT '用户角色:1-观众.2-参展商,name在代码里维护或用枚举维护',
  `PersonID` varchar(30) COLLATE utf8_croatian_ci DEFAULT NULL COMMENT '用户身份证号',
  `Telphone` varchar(20) COLLATE utf8_croatian_ci NOT NULL COMMENT '用户手机号',
  `Email` varchar(20) COLLATE utf8_croatian_ci NOT NULL COMMENT '用户Email',
  `CreateLanguage` smallint(6) DEFAULT NULL COMMENT '创建表单:1-中文.2-英语,name先不维护',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '注册时间',
  `IsDelete` smallint(6) DEFAULT 0 COMMENT '软删标识,1-删除',
  `CreateIP` varchar(20) COLLATE utf8_croatian_ci DEFAULT NULL COMMENT '预留字段(暂无不用)',
  `CompanyNameName` varchar(40) COLLATE utf8_croatian_ci DEFAULT NULL COMMENT '预留字段(暂无不用)',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_croatian_ci ROW_FORMAT = Compact;

# 业务表-首页频道设置表,这个表需要Insert初始记录,其中字典 FileName对应的路径(目录),用配置维护
CREATE TABLE `tbiz_module` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ModulePosition` varchar(10) NULL COMMENT '菜单坐标:约定1_1标识第1行,第1列',
  `ModuleName` varchar(30) COLLATE utf8_croatian_ci NULL COMMENT '菜单名',
  `ModuleOrder` smallint(6) COMMENT '菜单类别排序字段',
  `ModuleType` smallint(6) DEFAULT 1 COMMENT '菜单类别1-图片.2-pdf',
  `ModuleAction` smallint(6) DEFAULT 1 COMMENT '点击菜单行为1-表示显示,2-下载',
  `FileName` varchar(50) COLLATE utf8_croatian_ci NOT NULL COMMENT '按1对1设计时,保存1个对应的文件',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '注册时间',
  `IsDelete` smallint(6) DEFAULT 0 COMMENT '软删标识,1-删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_croatian_ci ROW_FORMAT = Compact;

# 预留表-防将来一个菜单显示多张图片的情况
CREATE TABLE `tbiz_module_detail` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ModuleID` int(255) NULL DEFAULT NULL COMMENT 'tbiz_module外键参考',
  `FileName` varchar(50) COLLATE utf8_croatian_ci NOT NULL COMMENT '按1对1设计,菜单对应1个附件文件',
  `CreateTime` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `IsDelete` smallint(6) DEFAULT 0 COMMENT '软删标识,1-删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET=utf8 COLLATE=utf8_croatian_ci ROW_FORMAT=COMPACT;
