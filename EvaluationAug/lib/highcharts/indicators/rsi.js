!/**
 * Highstock JS v11.4.8 (2024-08-29)
 *
 * Indicator series type for Highcharts Stock
 *
 * (c) 2010-2024 Paweł Fus
 *
 * License: www.highcharts.com/license
 */function(e){"object"==typeof module&&module.exports?(e.default=e,module.exports=e):"function"==typeof define&&define.amd?define("highcharts/indicators/rsi",["highcharts","highcharts/modules/stock"],function(t){return e(t),e.Highcharts=t,e}):e("undefined"!=typeof Highcharts?Highcharts:void 0)}(function(e){"use strict";var t=e?e._modules:{};function s(t,s,i,a){t.hasOwnProperty(s)||(t[s]=a.apply(null,i),"function"==typeof CustomEvent&&e.win.dispatchEvent(new CustomEvent("HighchartsModuleLoaded",{detail:{path:s,module:t[s]}})))}s(t,"Stock/Indicators/RSI/RSIIndicator.js",[t["Core/Series/SeriesRegistry.js"],t["Core/Utilities.js"]],function(e,t){let{sma:s}=e.seriesTypes,{isNumber:i,merge:a}=t;function n(e,t){return parseFloat(e.toFixed(t))}class r extends s{getValues(e,t){let s=t.period,a=e.xData,r=e.yData,o=r?r.length:0,u=t.decimals,d=[],c=[],h=[],l=0,f=0,p=t.index,m=1,g,y,x,j,v,S;if(!(a.length<s)){for(i(r[0])?S=r:(p=Math.min(p,r[0].length-1),S=r.map(e=>e[p]));m<s;)(y=n(S[m]-S[m-1],u))>0?l+=y:f+=Math.abs(y),m++;for(x=n(l/(s-1),u),j=n(f/(s-1),u),v=m;v<o;v++)(y=n(S[v]-S[v-1],u))>0?(l=y,f=0):(l=0,f=Math.abs(y)),x=n((x*(s-1)+l)/s,u),g=0===(j=n((j*(s-1)+f)/s,u))?100:0===x?0:n(100-100/(1+x/j),u),d.push([a[v],g]),c.push(a[v]),h.push(g);return{values:d,xData:c,yData:h}}}}return r.defaultOptions=a(s.defaultOptions,{params:{decimals:4,index:3}}),e.registerSeriesType("rsi",r),r}),s(t,"masters/indicators/rsi.src.js",[t["Core/Globals.js"]],function(e){return e})});