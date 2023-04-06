import { useNavigate } from "react-router-dom";
import jwt from "jwt-decode";
import {
  Col,
  Row,
  Button,
  Checkbox,
  Form,
  Input,
  Typography,
  Spin,
  message,
  Space,
} from "antd";
import { UserOutlined, LockOutlined, LoadingOutlined } from "@ant-design/icons";
import { fetchWrapper } from "../_helpers/fetchWrapper";
import LOGO from "./../assets/logo-delta-airlines.jpg";
import OIPFlight from "./../assets/OIPFlight.jpg";
import OIPBackGround1 from "./../assets/OIPBackground1.jpg";
import OIPLogo from "./../assets/Delta-Logo.svg";
import { useState } from "react";
import JoinMiles from "../components/JoinMiles/JoinMiles";
const { Title, Paragraph, Text, Link } = Typography;

function Login() {
  const layout = {
    labelCol: {
      xs: { span: 24 },
      sm: { span: 12 },
      md: { span: 8 },
      lg: { span: 8 },
    },
    wrapperCol: {
      xs: { span: 24 },
      sm: { span: 12 },
      md: { span: 12 },
      lg: { span: 12 },
    },
  };
  const tailLayout = {
    wrapperCol: {
      xs: { span: 24 },
      sm: { span: 12, offset: 12 },
      md: { span: 12, offset: 8 },
      lg: { span: 12, offset: 8 },
    },
  };
  const [loading, setLoading] = useState(false);

  const [result, setResult] = useState(false);

  const navigate = useNavigate();

  const antIcon = (
    <LoadingOutlined
      style={{
        fontSize: 24,
      }}
      spin
    />
  );

  const onFinish = (values) => {
    setLoading(true);

    fetchWrapper
      .post("/api/Auth/login", {
        email: values.username,
        password: values.password,
      })
      .then((data) => {
        if (data.success === true) {
          console.log(data.data);

          var token = data.data.accessToken;
          const user = jwt(token);

          message.success("Login Successfull!", 2);
          setResult(true);

          setTimeout(() => {
            window.localStorage.setItem("appuser", JSON.stringify(data.data));
            window.localStorage.setItem(
              "x-access-token",
              data.data.accessToken
            );
            navigate("/home/book");
          }, 1000);
        }
      })
      .catch((error) => {
        message.error(error);
      })
      .finally(() => {
        setLoading(false);
      });

    console.log("Success:", values);
  };
  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };

  return (
    <Spin
      tip="Redirecting.."
      indicator={antIcon}
      size="large"
      spinning={result}
    >
      <Row
      
      >
        <Col span={12}>
          <div style={{ backgroundColor: "black", height: "100%" }}>
            <img src={OIPBackGround1} className="opacity30 login-img" />
            <img src={OIPLogo} className="login-right-logo" />
            <h1 className="centered-img-text">Fly Delta Airlines</h1>
            <p className="bottom-text">Making you fly is our service</p>
          </div>
        </Col>
        <Col span={12}>
          <Row
            type="flex"
            justify="center"
            align="middle"
            style={{ minHeight: "100vh" }}
          >
            <Form

              name="basic"
              labelCol={{
                span: 8,
              }}
              style={{
                maxWidth: 800,
              }}
              initialValues={{
                remember: true,
              }}
              onFinish={onFinish}
              onFinishFailed={onFinishFailed}
              autoComplete="off"
            >
              <img src={LOGO} width="300px" />
              <Title level={3} style={{ textAlign: "center" }}>
                Login In to Delta
              </Title>
              <Form.Item
                name="username"
                rules={[
                  {
                    required: true,
                    message: "Please input your username!",
                  },
                ]}
              >
                <Input
                  size="large"
                  prefix={<UserOutlined />}
                  placeholder={"Email Id"}
                />
              </Form.Item>

              <Form.Item
                name="password"
                rules={[
                  {
                    required: true,
                    message: "Please input your password!",
                  },
                ]}
              >
                <Input.Password
                  size="large"
                  prefix={<LockOutlined />}
                  placeholder={"Password"}
                />
              </Form.Item>

              <Form.Item
                name="remember"
                valuePropName="checked"
                wrapperCol={{
                  offset: 2,
                  span: 16,
                }}
              >
                <Checkbox>Remember me</Checkbox>
              </Form.Item>

              <Form.Item
                wrapperCol={{
                  offset: 0,
                }}
              >
                <Space
                  direction="vertical"
                  style={{
                    width: "100%",
                  }}
                >
                  <Button
                    type="primary"
                    block
                    htmlType="submit"
                    loading={loading}
                  >
                    Submit
                  </Button>
                  <JoinMiles />
                  {/* <Button
                    block
                    className="google-button "
                    icon={
                      <img
                        width="20px"
                        style={{ marginRight: "8px" }}
                        alt="Google sign-in"
                        src="https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Google_%22G%22_Logo.svg/512px-Google_%22G%22_Logo.svg.png"
                      />
                    }
                  >
                    {" "}
                    Sign in with Google
                  </Button> */}
                </Space>
              </Form.Item>
            </Form>
          </Row>
        </Col>
      </Row>
    </Spin>
  );
}
export default Login;
