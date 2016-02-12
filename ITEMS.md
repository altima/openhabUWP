# Item

```javascript
{
	"link": "",
	"state": "",
	"type": "",
	"name": "",
	"label": "",
	"category": "",
	"tags": [],
	"groupNames": []
}
```

### StateDescription

```javascript
{
	"pattern": "%d %%",
	"readOnly": false,
	"options": []
}
```

## SwitchItem::[Item]

```javascript
{
	"link": "http://demo.openhab.org:9080/rest/items/DemoSwitch",
	"state": "OFF",
	"type": "SwitchItem",
	"name": "DemoSwitch",
	"label": "Switch",
	"tags": [],
	"groupNames": []
}
```

## DimmerItem::[Item]
```javascript
{
	"link": "http://demo.openhab.org:9080/rest/items/Volume",
	"state": "0",
	"stateDescription": {
		"pattern": "%.1f %%",
		"readOnly": false,
		"options": []
	},
	"type": "DimmerItem",
	"name": "Volume",
	"label": "Volume",
	"tags": [],
	"groupNames": []
}
```

## NumberItem::[Item]
## ColorItem::[Item]

```javascript
{
	"link": "http://demo.openhab.org:9080/rest/items/RGBLight",
	"state": "227.17557251908397,64.2156862745098,80",
	"type": "ColorItem",
	"name": "RGBLight",
	"label": "RGB Light",
	"category": "slider",
	"tags": [],
	"groupNames": []
}
```
## RollershutterItem::[Item]
```javascript
{
	"link": "http://demo.openhab.org:9080/rest/items/DemoShutter",
	"state": "100",
	"type": "RollershutterItem",
	"name": "DemoShutter",
	"label": "Roller Shutter",
	"tags": [],
	"groupNames": []
}
```

[Widget]: WIDGETS.md#widget
[Switch]: WIDGETS.md#switch::widget
[Chart]: WIDGETS.md#chart::widget
[Frame]: WIDGETS.md#frame::widget
[Selection]: WIDGETS.md#selection::widget
[Setpoint]: WIDGETS.md#setpoint::widget
[Slider]: WIDGETS.md#slider::widget
[Colorpicker]: WIDGETS.md#colorpicker::widget
[Mapview]: WIDGETS.md#mapview::widget
[Image]: WIDGETS.md#image::widget
[Video]: WIDGETS.md#video::widget
[Webview]: WIDGETS.md#webview::widget

[Item]: #item

[Mapping]: WIDGETS.md#mapping
