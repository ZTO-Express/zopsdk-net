# 中通开放平台SDK（.net版）

## 使用方式

```C#
// 这里的ZopProperties的构造方法的两个参数companyid和key，请到个人中心查看
ZopProperties properties = new ZopProperties("kfpttestCode","kfpttestkey==");
ZopClient client = new ZopClient(properties);
ZopRequest request = new ZopRequest();
request.url = "http://58.40.16.120:9001/submitOrderCode";
// addParam函数添加的是业务参数，每个接口的业务参数都不同，请查看API对应的文档描述
// 示例中的这个api只有一个名为data的业务参数
request.addParam("data", "{\"content\":{\"branchId\":\"\",\"buyer\":\"\",\"collectMoneytype\":\"CNY\",\"collectSum\":\"12.00\",\"freight\":\"10.00\",\"id\":\"xfs2018031500002222333\",\"orderSum\":\"0.00\",\"orderType\":\"1\",\"otherCharges\":\"0.00\",\"packCharges\":\"1.00\",\"premium\":\"0.50\",\"price\":\"126.50\",\"quantity\":\"2\",\"receiver\":{\"address\":\"育德路XXX号\",\"area\":\"501022\",\"city\":\"四川省,XXX,XXXX\",\"company\":\"XXXX有限公司\",\"email\":\"yyj@abc.com\",\"id\":\"130520142097\",\"im\":\"yangyijia-abc\",\"mobile\":\"136*****321\",\"name\":\"XXX\",\"phone\":\"010-222***89\",\"zipCode\":\"610012\"},\"remark\":\"请勿摔货\",\"seller\":\"\",\"sender\":{\"address\":\"华新镇华志路XXX号\",\"area\":\"310118\",\"city\":\"上海,上海市,青浦区\",\"company\":\"XXXXX有限公司\",\"email\":\"ll@abc.com\",\"endTime\":1369033200000,\"id\":\"131*****010\",\"im\":\"1924656234\",\"mobile\":\"1391***5678\",\"name\":\"XXX\",\"phone\":\"021-87***321\",\"startTime\":1369022400000,\"zipCode\":\"610012\"},\"size\":\"12,23,11\",\"tradeId\":\"2701843\",\"type\":\"1\",\"typeId\":\"\",\"weight\":\"0.753\"},\"datetime\":\"2018-3-30 12:00:00\",\"partner\":\"test\",\"verify\":\"ZTO123\"}");
Console.WriteLine(client.execute(request));
```

## 其他
1. 该项目刚创建，功能还很不完善，如有问题请提issue
2. 由于中通开放平台各个接口返回值格式没有统一，所以未封装返回值，未来可能会做封装