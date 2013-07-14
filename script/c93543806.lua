--先史遺産マッドゴーレム－シャコウキ
function c93543806.initial_effect(c)
	--lvchange
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(93543806,0))
	e1:SetCategory(CATEGORY_LVCHANGE)
	e1:SetType(EFFECT_TYPE_IGNITION)
	e1:SetRange(LOCATION_MZONE)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetCountLimit(1)
	e1:SetCost(c93543806.spcost)
	e1:SetTarget(c93543806.lvtg)
	e1:SetOperation(c93543806.lvop)
	c:RegisterEffect(e1)
	if not c93543806.global_check then
		c93543806.global_check=true
		local ge1=Effect.CreateEffect(c)
		ge1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
		ge1:SetCode(EVENT_SPSUMMON_SUCCESS)
		ge1:SetOperation(c93543806.checkop)
		Duel.RegisterEffect(ge1,0)
		local ge2=Effect.CreateEffect(c)
		ge2:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
		ge2:SetCode(EVENT_PHASE_START+PHASE_DRAW)
		ge2:SetOperation(c93543806.clear)
		Duel.RegisterEffect(ge2,0)
	end
end
function c93543806.spcost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return c93543806[tp] end
	local e1=Effect.CreateEffect(e:GetHandler())
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetProperty(EFFECT_FLAG_PLAYER_TARGET+EFFECT_FLAG_OATH)
	e1:SetCode(EFFECT_CANNOT_SPECIAL_SUMMON)
	e1:SetReset(RESET_PHASE+PHASE_END)
	e1:SetTargetRange(1,0)
	e1:SetTarget(c93543806.splimit)
	e1:SetLabelObject(e)
	Duel.RegisterEffect(e1,tp)
end
function c93543806.splimit(e,c,sump,sumtype,sumpos,targetp,se)
	return not c:IsSetCard(0x70)
end
function c93543806.lvfilter(c)
	return c:IsFaceup() and c:GetLevel()~=6
end
function c93543806.lvtg(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc then return chkc:IsLocation(LOCATION_MZONE) and chkc:IsControler(tp) and c93543806.lvfilter(chkc) end
	if chk==0 then return Duel.IsExistingTarget(c93543806.lvfilter,tp,LOCATION_MZONE,0,1,nil) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_FACEUP)
	Duel.SelectTarget(tp,c93543806.lvfilter,tp,LOCATION_MZONE,0,1,1,nil)
end
function c93543806.lvop(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	local tc=Duel.GetFirstTarget()
	if tc:IsRelateToEffect(e) and tc:IsFaceup() then
		local e1=Effect.CreateEffect(c)
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetCode(EFFECT_CHANGE_LEVEL)
		e1:SetValue(6)
		e1:SetReset(RESET_EVENT+0x1fe0000)
		tc:RegisterEffect(e1)
	end
end
function c93543806.checkop(e,tp,eg,ep,ev,re,r,rp)
	local tc=eg:GetFirst()
	while tc do
		if not tc:IsSetCard(0x70) then
			c93543806[tc:GetSummonPlayer()]=false
		end
		tc=eg:GetNext()
	end
end
function c93543806.clear(e,tp,eg,ep,ev,re,r,rp)
	c93543806[0]=true
	c93543806[1]=true
end