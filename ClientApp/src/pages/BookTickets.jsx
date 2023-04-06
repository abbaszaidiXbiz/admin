import {
  Button,
  Form,
  Input,
  Radio,
  AutoComplete,
  Row,
  Col,
  message,
  DatePicker,
  Space,
  Select,
  InputNumber,
  Card,
} from "antd";
import {
  LoadingOutlined,
  SwapRightOutlined,
  SwapLeftOutlined,
} from "@ant-design/icons";
import { fetchWrapper } from "../_helpers/fetchWrapper";
import { useState } from "react";
import OIPBackGround from "./../assets/OIPBackground.jpg";
const { RangePicker } = DatePicker;

const BookTickets = () => {
  const [form] = Form.useForm();

  const [state, setState] = useState({
    tripType: "return",
    source: "",
    destination: "",
    fromDate: "",
    toDate: "",
    loading: false,
    options: [],
    rangePickerHidden: true,
    datePickerHidden: true,
  });

  const [result, setResult] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleChange = (value) => {
    console.log(`selected ${value}`);
  };

  const onSelectSource = (data) => {
    if (data === state.destination) {
      message.info("Destination and source cannot be the same.");
      setState({ ...state, options: [] });
      return;
    }
    setState({ ...state, source: data });
  };

  const onSelectDestination = (data) => {
    if (data === state.source) {
      message.info("Destination and source cannot be the same.");
      setState({ ...state, options: [] });
      return;
    }
  };

  const setAirports = (value) => {
    fetchWrapper
      .post("/api/Flight/airports/search", { searchText: value })
      .then((data) => {
        if (data.success === true) {
          var airportData = data.data;
          state.options = airportData.map((item) => ({
            label: `${item.name},(${item.country})`,
            value: item.airportCode,
          }));
        }
      })
      .catch((error) => {})
      .finally(() => {
        setState({ ...state, loading: false });
      });
  };
  const handleFromSearch = (value) => {
    setState({ ...state, options: [] });

    if (!value) {
      state.options = [];
    } else {
      setAirports(value);
    }
  };
  const handleToSearch = (value) => {
    setState({ ...state, options: [] });

    if (!value) {
      state.options = [];
    } else {
      setAirports(value);
    }
  };
  const onDatePickerChange = (value, dateString) => {
    setState({ ...state, fromDate: dateString });
  };

  const onChange = (value, dateString) => {
    console.log("onChange", dateString);

    setState({ ...state, fromDate: dateString[0], toDate: dateString[1] });
  };

  const onTypeChange = (e) => {
    let type = e.target.value;

    if (type === "oneway") {
      state.rangePickerHidden = true;
      state.datePickerHidden = false;
    } else {
      state.rangePickerHidden = false;
      state.datePickerHidden = true;
    }
    setState({ ...state, tripType: type });
  };

  const onValuesChange = () => {};

  const onFinish = (values) => {
    console.log(values);
  };
  const onFinishFailed = (errorInfo) => {
    console.log("Failed:", errorInfo);
  };

  return (
    <div
      style={{
        backgroundImage: `url(${OIPBackGround})`,
        backgroundRepeat: "no-repeat",
        height: "100vh",
        backgroundSize: "cover",
      }}
    >
      <Card className="child">
        <Form
          layout="horizontal"
          form={form}
          onValuesChange={onValuesChange}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          style={{
            maxWidth: 600,
          }}
        >
          <Form.Item
            label="Trip type"
            name="type"
            rules={[
              {
                required: true,
                message: "Please select Trip type!",
              },
            ]}
          >
            <Radio.Group
              onChange={onTypeChange}
              defaultValue="oneway"
              buttonStyle="solid"
            >
              <Radio.Button value="return">Return</Radio.Button>
              <Radio.Button value="oneway">One-way</Radio.Button>
              <Radio.Button value="multicity">MultiCity</Radio.Button>
            </Radio.Group>
          </Form.Item>

          <Form.Item label="Select">
            <Space>
              <Form.Item name="source">
                <AutoComplete
                  popupClassName="certain-category-search-dropdown"
                  dropdownMatchSelectWidth={500}
                  onSearch={handleFromSearch}
                  onSelect={onSelectSource}
                  options={state.options}
                >
                  <Input
                    name="source"
                    size="large"
                    placeholder="Source..."
                    prefix={<SwapRightOutlined />}
                  />
                </AutoComplete>
              </Form.Item>

              <Form.Item name="destination">
                <AutoComplete
                  popupClassName="certain-category-search-dropdown"
                  dropdownMatchSelectWidth={500}
                  onSearch={handleToSearch}
                  onSelect={onSelectDestination}
                  options={state.options}
                >
                  <Input
                    prefix={<SwapLeftOutlined />}
                    size="large"
                    placeholder="destination..."
                  />
                </AutoComplete>
              </Form.Item>
            </Space>
          </Form.Item>

          <Form.Item label="passengers">
            <Space>
              <Form.Item name="adults">
                <InputNumber min={0} max={10} placeholder="Adults" />
              </Form.Item>
              <Form.Item name="children">
                <InputNumber min={0} max={10} placeholder="Children" />
              </Form.Item>
              <Form.Item wrapperCol={{ span: 16 }} name="infants">
                <InputNumber min={0} max={10} placeholder="infant below 2yrs" />
              </Form.Item>
            </Space>
          </Form.Item>

          <Form.Item
            hidden={state.rangePickerHidden}
            label="Select Depart and Return Date"
          >
            <Space direction="vertical" size={12}>
              <RangePicker
                name="dates"
                placeholder={["Depart", "Arrival"]}
                onChange={onChange}
              />
            </Space>
          </Form.Item>

          <Form.Item hidden={state.datePickerHidden} label="Select Depart Date">
            <Space direction="vertical" size={12}>
              <DatePicker onChange={onDatePickerChange} />
            </Space>
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit">
              Search Flights
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </div>
  );
};
export default BookTickets;
