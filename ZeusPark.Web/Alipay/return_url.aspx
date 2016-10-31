<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="return_url.aspx.cs" Inherits="ZeusParkNew.return_url" %>


<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<head>
	<title>ZEUSPARK</title>

	<!-- Meta -->
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<meta name="description" content="">
	<meta name="author" content="">

	<!-- Favicon -->
	<link rel="shortcut icon" href="favicon.ico">

	<!-- Web Fonts -->
	<link rel="stylesheet" type="text/css" href="//fonts.googleapis.com/css?family=Open+Sans:400,300,700,200&amp;subset=cyrillic,latin">

	<!-- CSS Global Compulsory -->
	<link rel="stylesheet" href="http://asset.zeuspark.com/assets/plugins/bootstrap/css/bootstrap.min.css">
	<link rel="stylesheet" href="http://asset.zeuspark.com/assets/css/style.css">

	<!-- CSS Header and Footer -->
	<link rel="stylesheet" href="http://asset.zeuspark.com/assets/css/headers/header-default.css">
	<link rel="stylesheet" href="http://asset.zeuspark.com/assets/css/footers/footer-v1.css">

	<!-- CSS Implementing Plugins -->
	<link rel="stylesheet" href="http://asset.zeuspark.com/assets/plugins/animate.css">
	<link rel="stylesheet" href="http://asset.zeuspark.com/assets/plugins/line-icons/line-icons.css">
	<link rel="stylesheet" href="http://asset.zeuspark.com/assets/plugins/font-awesome/css/font-awesome.min.css">

	<!-- CSS Page Style -->
	<link rel="stylesheet" href="http://asset.zeuspark.com/assets/css/pages/page_404_error5.css">

</head>

<body>
	<!--=== Error V6 ===-->
	<div class="container valign__middle">
		<!--Error Block-->
		<div class="row">
			<div class="col-md-8 col-md-offset-2">
				<div class="error-v6 valign__middle">
					<a href="http://www.zeuspark.com/" class="logo-a"><img class="logo" src="http://asset.zeuspark.com/site/zlogo.png" alt=""></a>
					<h1>订单支付成功！</h1>
                    <asp:Label ID="theLabel" runat="server" class="sorry" />
					<span class="input-group-btn">
						<a href="http://www.zeuspark.com/#/myorder" class="btn-u btn-u-red" style="width:250px">返回 ZEUSPARK 查看我的订单</a>
					</span>
				</div>
			</div>
		</div>
	</div><!--/container-->
	<!--End Error Block-->

	<!--=== Sticky Footer ===-->
	<div class="container sticky-footer">
		<p class="copyright-space">
			 Copyright ©ZEUSPARK 2011-2020 <a href="http://www.zeuspark.com/" target="_blank" class="color-red">沪ICP备14014110号-3</a>
		</p>
	</div>
	<!--=== End Sticky Footer ===-->

	<!-- JS Global Compulsory -->
	<script src="http://asset.zeuspark.com/assets/plugins/jquery/jquery.min.js"></script>
	<script src="http://asset.zeuspark.com/assets/plugins/jquery/jquery-migrate.min.js"></script>
	<script src="http://asset.zeuspark.com/assets/plugins/bootstrap/js/bootstrap.min.js"></script>

	<!-- JS Implementing Plugins -->
	<script src="http://asset.zeuspark.com/assets/plugins/back-to-top.js"></script>

	<!-- JS Page Level -->
	<script src="http://asset.zeuspark.com/assets/js/app.js"></script>
	<script>
		jQuery(document).ready(function() {
			App.init();
		});
	</script>
	<!--[if lt IE 9]>
		<script src="assets/plugins/respond.js"></script>
		<script src="assets/plugins/html5shiv.js"></script>
		<script src="assets/plugins/placeholder-IE-fixes.js"></script>
		<![endif]-->
	</body>
</html>
