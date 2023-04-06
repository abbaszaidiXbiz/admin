import {
  LoginOutlined,
  LogoutOutlined,
  VideoCameraOutlined,
  DingdingOutlined,
  DatabaseOutlined,
  MailOutlined,
  UserOutlined,
} from "@ant-design/icons";
import { Breadcrumb, Layout, Menu, theme, Button, Space } from "antd";
import Logo from "./assets/OIPLogo.jpg";
import { Outlet, Link, useFetcher } from "react-router-dom";

import React from "react";
import { useEffect } from "react";
const { Header, Content, Sider } = Layout;

let token = window.localStorage.getItem("x-access-token");
let appuser = JSON.parse(window.localStorage.getItem("appuser"));

const items1 = [
  {
    label: <Link to={"/home"}>Flights</Link>,
    key: "All",
    icon: <DingdingOutlined />,
  },
  {
    label: "Hotel",
    key: "",
    icon: <DatabaseOutlined />,
    disabled: true,
  },
];

if (appuser !== null) {
  const authNav = {
    label: appuser === null ? {} : <a href="#">{appuser.skyMilesId}</a>,
    icon: token === null ? <UserOutlined /> : <UserOutlined />,
    key: "skyMilesId",
    children: [
      {
        label: appuser.email,
        key: "email",
      },
      {
        label: (
          <a href="#" onClick={() => window.localStorage.clear()}>
            Logout
          </a>
        ),
        icon: <LogoutOutlined />,
        key: "logout",
      },
    ],
  };

  items1.push(authNav);
}
if (token !== null) {
  const userNav = {
    label:
      token === null ? (
        <Link to={"/"}>Login</Link>
      ) : (
        <Link to={"/"}>Logout</Link>
      ),
    icon: token === null ? <LoginOutlined /> : <LogoutOutlined />,
    key: "email",
  };

  items1.push(userNav);
}

const App = () => {
  const {
    token: { colorBgContainer },
  } = theme.useToken();
  return (
    <Layout>
      <Header className="header">
        <img alt="logo" className="logo" src={Logo} />
        <Menu mode="horizontal" items={items1} theme={"dark"}></Menu>
      </Header>
      <Layout>
        {/* <Sider
          width={200}
          style={{
            background: colorBgContainer,
          }}
        >
          <Menu
            mode="inline"
            defaultSelectedKeys={["1"]}
            defaultOpenKeys={["sub1"]}
            style={{
              height: "100%",
              borderRight: 0,
            }}
            items={items2}
          />
        </Sider> */}
        <Layout>
          <Content>
            <Outlet />
          </Content>
        </Layout>
      </Layout>
    </Layout>
  );
};
export default App;
