/// <reference path="./KeyValuePair.ts"/>

interface Location {
    GetUrlParameters(): Array<KeyValuePair>;
    GetUrlParameterValue(key: string): string;
}

Location.prototype.GetUrlParameters = function (): Array<KeyValuePair> {
    var result = new Array<KeyValuePair>();

    if (this.search.length > 0) {
        let segments = this.search.substring(1).split('&') as Array<string>;

        segments.forEach(bajs => {
            let parts = bajs.split('=');

            let param = new KeyValuePair();
            param.key = parts[0];
            if (parts.length > 1) {
                param.value = parts[1];
            }
            else {
                param.value = null;
            }

            result.push(param);
        })
    }

    return result;
} 

Location.prototype.GetUrlParameterValue = function (key: string) {
    var params = this.GetUrlParameters() as Array<KeyValuePair>;
    let result = null as string;

    params.forEach(param => {
        if (param.key == key) {
            result = param.value;
        }
    });

    return result;
}