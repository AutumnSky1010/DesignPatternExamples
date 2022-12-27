using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFDesignPatternExamples.Creational.SingletonPattern;
// Loggerを使うクラスの例
class SingletonUser : IUser
{
    public void Use()
    {
        /** 
		 * コンストラクタは隠蔽されているので、エラーが発生する。
		 * var logger = new Logger();
		 */
        var logger = Logger.Instance;
        logger.ShowWithTime("出力する");
    }
}
