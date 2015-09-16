/*
Navicat MySQL Data Transfer

Source Server         : nele
Source Server Version : 50520
Source Host           : 127.0.0.1:3306
Source Database       : qx

Target Server Type    : MYSQL
Target Server Version : 50520
File Encoding         : 65001

Date: 2015-02-07 23:19:34
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_department
-- ----------------------------
DROP TABLE IF EXISTS `t_department`;
CREATE TABLE `t_department` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `departmentname` longtext,
  `remark` longtext,
  `isdelete` tinyint(1) NOT NULL,
  `addtime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_department
-- ----------------------------
INSERT INTO `t_department` VALUES ('1', '财务部', '管理财务', '0', '2015-02-02 22:19:50');
INSERT INTO `t_department` VALUES ('2', '人事部', null, '0', '2015-02-07 10:57:19');

-- ----------------------------
-- Table structure for t_permission
-- ----------------------------
DROP TABLE IF EXISTS `t_permission`;
CREATE TABLE `t_permission` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `parent_id` int(11) DEFAULT NULL,
  `pname` longtext,
  `permission_area_name` longtext,
  `permission_controller_name` longtext,
  `permisson_action_name` longtext,
  `permisson_form_method` int(11) DEFAULT NULL,
  `purl` longtext,
  `isshow` tinyint(1) NOT NULL,
  `isdelete` tinyint(1) NOT NULL,
  `remark` longtext,
  `addtime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `parent_id` (`parent_id`),
  CONSTRAINT `Permission_Father` FOREIGN KEY (`parent_id`) REFERENCES `t_permission` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_permission
-- ----------------------------
INSERT INTO `t_permission` VALUES ('1', null, '学生管理', null, 'Student', 'Index', '0', '/Student/Index', '1', '0', null, '2015-02-06 11:12:29');
INSERT INTO `t_permission` VALUES ('2', '1', '增加学生', null, 'Student', 'Add', '0', '/Student/Add', '1', '0', null, '2015-02-06 11:49:06');
INSERT INTO `t_permission` VALUES ('3', '1', '删除学生', null, 'Student', 'Delete', '0', '/Stundet/Delete', '1', '1', '删除学生权限', '2015-02-06 15:02:23');
INSERT INTO `t_permission` VALUES ('4', null, '消息管理', null, 'Info', 'Index', '0', '/Info/Index', '1', '0', '系统内部消息管理', '2015-02-06 15:03:47');
INSERT INTO `t_permission` VALUES ('5', '4', '写消息', null, 'Info', 'Send', '0', '/Info/Send', '1', '0', null, '2015-02-06 15:05:45');
INSERT INTO `t_permission` VALUES ('6', '2', 'test', null, 'Student', 'Test', '0', '/Student/Test', '1', '1', null, '2015-02-06 15:44:43');
INSERT INTO `t_permission` VALUES ('8', '1', '删除学生', null, 'Student', 'Delete', '0', '/Student/Delete', '1', '0', null, '2015-02-07 00:20:35');

-- ----------------------------
-- Table structure for t_role
-- ----------------------------
DROP TABLE IF EXISTS `t_role`;
CREATE TABLE `t_role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `department_id` int(11) NOT NULL,
  `role_name` longtext,
  `remark` longtext,
  `isshow` tinyint(1) NOT NULL,
  `isdelete` tinyint(1) NOT NULL,
  `addtime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `department_id` (`department_id`),
  CONSTRAINT `Role_Department` FOREIGN KEY (`department_id`) REFERENCES `t_department` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_role
-- ----------------------------
INSERT INTO `t_role` VALUES ('1', '1', '财务部普通职员', '普通财务管理', '1', '0', '2015-02-06 15:50:00');
INSERT INTO `t_role` VALUES ('2', '2', '普通管理员', null, '1', '0', '2015-02-07 16:48:25');
INSERT INTO `t_role` VALUES ('3', '1', 'test', null, '1', '0', '2015-02-07 16:48:43');

-- ----------------------------
-- Table structure for t_role_permission
-- ----------------------------
DROP TABLE IF EXISTS `t_role_permission`;
CREATE TABLE `t_role_permission` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `rid` int(11) NOT NULL,
  `pid` int(11) NOT NULL,
  `IsDelete` tinyint(1) NOT NULL,
  `addtime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `pid` (`pid`),
  KEY `rid` (`rid`),
  CONSTRAINT `RolePermission_Permission` FOREIGN KEY (`pid`) REFERENCES `t_permission` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `RolePermission_Role` FOREIGN KEY (`rid`) REFERENCES `t_role` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_role_permission
-- ----------------------------
INSERT INTO `t_role_permission` VALUES ('3', '1', '1', '0', '2015-02-07 00:14:46');
INSERT INTO `t_role_permission` VALUES ('4', '1', '2', '0', '2015-02-07 00:14:46');
INSERT INTO `t_role_permission` VALUES ('5', '1', '4', '0', '2015-02-07 00:14:46');
INSERT INTO `t_role_permission` VALUES ('6', '1', '5', '0', '2015-02-07 00:14:46');

-- ----------------------------
-- Table structure for t_user
-- ----------------------------
DROP TABLE IF EXISTS `t_user`;
CREATE TABLE `t_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `department_id` int(11) DEFAULT NULL,
  `username` longtext,
  `password` longtext,
  `addtime` datetime NOT NULL,
  `isdelete` tinyint(1) NOT NULL,
  `remark` longtext,
  `gender` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `department_id` (`department_id`),
  CONSTRAINT `User_Department` FOREIGN KEY (`department_id`) REFERENCES `t_department` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_user
-- ----------------------------
INSERT INTO `t_user` VALUES ('1', '1', 'admin', 'E10ADC3949BA59ABBE56E057F20F883E', '2015-02-02 22:20:24', '0', null, '0');
INSERT INTO `t_user` VALUES ('3', '1', 'test', '96e79218965eb72c92a549dd5a330112', '2015-02-06 11:08:35', '0', null, '0');
INSERT INTO `t_user` VALUES ('5', '2', 'xiaoming', '96e79218965eb72c92a549dd5a330112', '2015-02-07 23:11:44', '0', null, '0');

-- ----------------------------
-- Table structure for t_user_role
-- ----------------------------
DROP TABLE IF EXISTS `t_user_role`;
CREATE TABLE `t_user_role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL,
  `rid` int(11) NOT NULL,
  `isdelete` tinyint(1) NOT NULL,
  `addtime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `rid` (`rid`),
  KEY `uid` (`uid`),
  CONSTRAINT `UserRole_Role` FOREIGN KEY (`rid`) REFERENCES `t_role` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `UserRole_User` FOREIGN KEY (`uid`) REFERENCES `t_user` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_user_role
-- ----------------------------
INSERT INTO `t_user_role` VALUES ('1', '3', '1', '0', '2015-02-07 20:45:13');

-- ----------------------------
-- Table structure for t_user_vip_permission
-- ----------------------------
DROP TABLE IF EXISTS `t_user_vip_permission`;
CREATE TABLE `t_user_vip_permission` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL,
  `pid` int(11) NOT NULL,
  `isDelete` tinyint(1) NOT NULL,
  `remark` longtext,
  `addtime` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `pid` (`pid`),
  KEY `uid` (`uid`),
  CONSTRAINT `UserVipPermission_Permission` FOREIGN KEY (`pid`) REFERENCES `t_permission` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `UserVipPermission_User` FOREIGN KEY (`uid`) REFERENCES `t_user` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_user_vip_permission
-- ----------------------------

-- ----------------------------
-- Table structure for __migrationhistory
-- ----------------------------
DROP TABLE IF EXISTS `__migrationhistory`;
CREATE TABLE `__migrationhistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ContextKey` varchar(300) NOT NULL,
  `Model` longblob NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of __migrationhistory
-- ----------------------------
