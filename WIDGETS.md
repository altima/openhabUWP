# Widget

```javascript
{
	"widgetId": "",
	"type": "",
	"label": "",
	"icon": "",
	"mappings": [],
	"item": { },
	"widgets": []
}
```

### Mapping

```javascript
{
	"command": "ON",
	"label": "On"
}
```


## Frame::[Widget]
```javascript
{
	"widgetId": "0202_0",
	"type": "Frame",
	"label": "Binary Widgets",
	"icon": "frame",
	"mappings": [],
	"widgets": [{
		"widgetId": "0202_0_0",
		"type": "Switch",
		"label": "Toggle Switch",
		"icon": "switch",
		"mappings": [],
		"item": {
			"link": "http://demo.openhab.org:9080/rest/items/DemoSwitch",
			"state": "OFF",
			"type": "SwitchItem",
			"name": "DemoSwitch",
			"label": "Switch",
			"tags": [],
			"groupNames": []
		},
		"widgets": []
	},
	{
		"widgetId": "0202_0_0_1",
		"type": "Switch",
		"label": "Button Switch",
		"icon": "switch",
		"mappings": [{
			"command": "ON",
			"label": "On"
		}],
		"item": {
			"link": "http://demo.openhab.org:9080/rest/items/DemoSwitch",
			"state": "OFF",
			"type": "SwitchItem",
			"name": "DemoSwitch",
			"label": "Switch",
			"tags": [],
			"groupNames": []
		},
		"widgets": []
	}]
}
```


## Switch::[Widget]
### w/o mappings
```javascript
{
	"widgetId": "0202_0_0",
	"type": "Switch",
	"label": "Toggle Switch",
	"icon": "switch",
	"mappings": [],
	"item": {
		"link": "http://demo.openhab.org:9080/rest/items/DemoSwitch",
		"state": "OFF",
		"type": "SwitchItem",
		"name": "DemoSwitch",
		"label": "Switch",
		"tags": [],
		"groupNames": []
	},
	"widgets": []
}
```
### with mappings
```javascript
{
	"widgetId": "0202_0_0_1",
	"type": "Switch",
	"label": "Button Switch",
	"icon": "switch",
	"mappings": [{
		"command": "ON",
		"label": "On"
		}],
	"item": {
		"link": "http://demo.openhab.org:9080/rest/items/DemoSwitch",
		"state": "OFF",
		"type": "SwitchItem",
		"name": "DemoSwitch",
		"label": "Switch",
		"tags": [],
		"groupNames": []
	},
	"widgets": []
}
```
### Rollershutter
```javascript
{
	"widgetId": "0202_2_0_1_2",
	"type": "Switch",
	"label": "Roller Shutter",
	"icon": "rollershutter",
	"mappings": [],
	"item": {
		"link": "http://demo.openhab.org:9080/rest/items/DemoShutter",
		"state": "100",
		"type": "RollershutterItem",
		"name": "DemoShutter",
		"label": "Roller Shutter",
		"tags": [],
		"groupNames": []
	},
	"widgets": []
}
```

## Chart::[Widget]
```javascript

```


## Selection::[Widget]
```javascirpt
{
	"widgetId": "0203_0_0",
	"type": "Selection",
	"label": "Radio",
	"icon": "network",
	"mappings": [{
		"command": "0",
		"label": "off"
	},
	{
		"command": "1",
		"label": "HR3"
	},
	{
		"command": "2",
		"label": "SWR3"
	},
	{
		"command": "3",
		"label": "FFH"
	}],
	"item": {
		"link": "http://demo.openhab.org:9080/rest/items/Radio_Station",
		"state": "2",
		"type": "NumberItem",
		"name": "Radio_Station",
		"label": "Radio",
		"category": "network",
		"tags": [],
		"groupNames": []
	},
	"widgets": []
}
```
## Setpoint::[Widget]
```javascript
{
	"widgetId": "0202_1_0_1_2",
	"type": "Setpoint",
	"label": "Temperature [20.5 Â°C]",
	"icon": "temperature",
	"mappings": [],
	"minValue": 16,
	"maxValue": 28,
	"step": 0.5,
	"item": {
		"link": "http://demo.openhab.org:9080/rest/items/Temperature_Setpoint",
		"state": "20.5",
		"stateDescription": {
			"pattern": "%.1f Â°C",
			"readOnly": false,
			"options": []
		},
		"type": "NumberItem",
		"name": "Temperature_Setpoint",
		"label": "Temperature",
		"category": "temperature",
		"tags": [],
		"groupNames": []
	},
	"widgets": []
}```

## Slider::[Widget]
```javascript
{
	"widgetId": "0202_2_0",
	"type": "Slider",
	"label": "Dimmer [100 %]",
	"icon": "slider",
	"mappings": [],
	"switchSupport": true,
	"sendFrequency": 0,
	"item": {
		"link": "http://demo.openhab.org:9080/rest/items/DimmedLight",
		"state": "100",
		"stateDescription": {
			"pattern": "%d %%",
			"readOnly": false,
			"options": []
		},
		"type": "DimmerItem",
		"name": "DimmedLight",
		"label": "Dimmer",
		"category": "slider",
		"tags": [],
		"groupNames": []
	},
	"widgets": []
}
```

## Colorpicker::[Widget]
```javascript
{
	"widgetId": "0202_2_0_1",
	"type": "Colorpicker",
	"label": "RGB Light",
	"icon": "slider",
	"mappings": [],
	"item": {
		"link": "http://demo.openhab.org:9080/rest/items/RGBLight",
		"state": "227.17557251908397,64.2156862745098,80",
		"type": "ColorItem",
		"name": "RGBLight",
		"label": "RGB Light",
		"category": "slider",
		"tags": [],
		"groupNames": []
	},
	"widgets": []
}
```
## Mapview::[Widget]
```javascript
{
	"widgetId": "0202_3",
	"type": "Frame",
	"label": "Map/Location",
	"icon": "frame",
	"mappings": [],
	"widgets": [{
		"widgetId": "0202_3_0",
		"type": "Mapview",
		"label": "Brandenburg Gate Berlin",
		"icon": "mapview",
		"mappings": [],
		"height": 10,
		"item": {
			"link": "http://demo.openhab.org:9080/rest/items/DemoLocation",
			"state": "52.5200066,13.4049540",
			"type": "LocationItem",
			"name": "DemoLocation",
			"label": "Brandenburg Gate Berlin",
			"tags": [],
			"groupNames": []
		},
		"widgets": []
	}]
}
```
## Image::[Widget]
```javascript
{
	"widgetId": "0203_1_0",
	"type": "Image",
	"label": "openHAB",
	"icon": "image",
	"mappings": [],
	"url": "http://demo.openhab.org:9080/proxy?sitemap=demo.sitemap&widgetId=02030100",
	"linkedPage": {
		"id": "02030100",
		"title": "openHAB",
		"icon": "image",
		"link": "http://demo.openhab.org:9080/rest/sitemaps/demo/02030100",
		"leaf": true
	},
	"widgets": []
}
```
## Video::[Widget]
```javascript
{
	"widgetId": "0203_1_0_1",
	"type": "Video",
	"label": "",
	"icon": "video",
	"mappings": [],
	"url": "http://demo.openhab.org:9080/proxy?sitemap=demo.sitemap&widgetId=02030101",
	"widgets": []
}
```

## Webview::[Widget]

```javascript
{
	"widgetId": "0203_1_0_1_2",
	"type": "Webview",
	"label": "",
	"icon": "webview",
	"mappings": [],
	"height": 8,
	"url": "http://heise-online.mobi/",
	"widgets": []
}
```

[Widget] : #widget
[Switch] : #switch::widget
[Chart] : #chart::widget
[Frame] : #frame::widget
[Selection] : #selection::widget
[Setpoint] : #setpoint::widget
[Slider] : #slider::widget
[Colorpicker] : #colorpicker::widget
[Mapview] : #mapview::widget
[Image] : #image::widget
[Video] : #video::widget
[Webview] : #webview::widget

[Item] : ITEMS.md#item

[Mapping] : #mapping
